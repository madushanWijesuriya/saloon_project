<template>
  <div class="manage-orders mx-6">
    <h2 class="mb-5">Manage Orders</h2>
    <v-data-table
      :headers="ordersHeaders"
      :items="ordersData"
      :loading="ordersLoading"
      class="elevation-1"
    >
      <template v-slot:top>
        <v-toolbar flat>
          <v-divider class="mx-4" inset vertical></v-divider>
          <v-spacer></v-spacer>

          <v-dialog v-model="dialogStatus" max-width="500px">
            <v-card>
              <v-card-title class="text-h5">Change status</v-card-title>
              <v-card-text>
                <v-radio-group v-model="statusSelected" column>
                  <v-radio label="Placed" color="orange" :value="0"></v-radio>
                  <v-radio
                    :value="1"
                    label="Approved"
                    color="red darken-3"
                  ></v-radio>
                  <v-radio
                    label="Paid"
                    color="orange darken-3"
                    :value="2"
                  ></v-radio>
                  <v-radio
                    label="Canceled"
                    color="gray darken-3"
                    :value="3"
                  ></v-radio>
                </v-radio-group>
              </v-card-text>
              <v-divider class="mx-4"></v-divider>
              <h5 class="red--text ml-15 my-3" v-if="isStatusBtnDisabled">
                Cannot save status as paid, use geneare invoice instead
              </h5>
              <v-card-actions>
                <v-spacer></v-spacer>
                <v-btn color="gray" @click="closeDialogStatus">Cancel</v-btn>
                <v-btn
                  color="#56896c"
                  class="ml-5 white--text"
                  @click="dialogStatusConfirm"
                  :disabled="isStatusBtnDisabled"
                  >OK</v-btn
                >
                <v-spacer></v-spacer>
              </v-card-actions>
            </v-card>
          </v-dialog>
        </v-toolbar>
      </template>

      <template v-slot:item.actions="{ item }">
        <v-btn
          v-if="viewChangeStatus(item)"
          small
          @click="changeStatus(item)"
          color="green"
          class="white--text"
          >Change Status</v-btn
        >
        <span>
          <v-btn
            v-if="generateInvoiceShow(item)"
            small
            @click="generateInvoice(item)"
            color="blue"
            class="white--text ml-5"
            >Generate Invoice</v-btn
          >
          <v-btn
            v-if="viewInvoiceShow(item)"
            small
            @click="viewInvoices(item)"
            color="orange"
            class="white--text ml-5"
            >View Invoices</v-btn
          >
        </span>
      </template>
    </v-data-table>
    <ViewOrderInvoices
      v-if="selectedOrderForInvoiceView"
      :order="selectedOrderForInvoiceView"
      ref="invoiceModalRef"
    />
    <AddAmount ref="addAmountRef" @generate-invoice="generateInvoiceConfirm" />
  </div>
</template>

<script>
import order from '@/assets/js/api/order'
import moment from 'moment'
import { orderStatus } from '@/assets/js/enums/orderEnum'
import objectHelper from '@/assets/js/healpers/objectHelper'
import AddAmount from '@/components/orders/models/AddAmount.vue'
import ViewOrderInvoices from '@/components/orders/models/ViewOrderInvoices.vue'
export default {
  name: 'ManageOrders',
  mixins: [order, objectHelper],
  data: () => ({
    selectedOrderForInvoiceView: null,
    selectedItem: null,
    ordersData: [],
    ordersLoading: false,
    ordersHeaders: [
      {
        text: 'Created Date',
        align: 'start',
        sortable: true,
        value: 'createdDateFormatted'
      },
      { text: 'Mobile No', value: 'mobileNo', sortable: true },
      { text: 'Order Status', value: 'orderStatusText', sortable: true },
      { text: 'Total Amount', value: 'totalAmount', sortable: true },
      { text: 'Actions', value: 'actions', sortable: false, align: 'center' }
    ],
    statusSelected: 0,
    dialogStatus: false
  }),
  methods: {
    generateInvoiceShow(item) {
      return (
        item.orderStatus === 1 ||
        item.orderStatus === 2 ||
        item.orderStatus === 3
      )
    },
    viewInvoiceShow(item) {
      return item.orderStatus === 2 || item.orderStatus === 3
    },
    viewChangeStatus(item) {
      return item.orderStatus === 0 || item.orderStatus === 1
    },
    generateInvoice(item) {
      this.selectedItem = item
      this.$refs.addAmountRef.openModal()
    },
    generateInvoiceConfirm(amount) {
      const item = this.selectedItem
      this.GenerateInvoiceForOrder(
        {
          url: `/Orders/${item.id}/generateOrderInvoice`,
          method: 'POST',
          data: {
            Amount: amount
          }
        },
        () => {
          this.$toast.success('Successfully generated invoice for the order')
          this.$refs.addAmountRef.closeModal()
        },
        error => {
          console.error(error)
        }
      )
    },
    viewInvoices(item) {
      console.log(item)
      this.selectedOrderForInvoiceView = item
      setTimeout(() => {
        this.$refs.invoiceModalRef.openModal()
      }, 100)
    },
    getAllOrders() {
      this.ordersLoading = true
      this.getOrders(
        {
          url: '/Orders',
          method: 'GET'
        },
        response => {
          this.ordersData = response.data.map(d => {
            return {
              ...d,
              createdDateFormatted: moment(
                new Date(Date.parse(d.createdDate))
              ).format('MMM DD YYYY'),
              orderStatusText: this.getKeyByValue(orderStatus, d.orderStatus)
            }
          })
          this.ordersLoading = false
        },
        error => {
          this.ordersLoading = false
          console.error(error)
        }
      )
    },
    dialogStatusConfirm() {
      const orderId = this.selectedItem.id
      const data = {
        Status: this.statusSelected
      }
      const orderToUpdate = this.selectedItem
      this.ChangeOrderStatus(
        {
          url: `Orders/${orderId}/orderStatusChange`,
          method: 'PUT',
          data: data
        },
        () => {
          orderToUpdate.orderStatus = data.Status
          orderToUpdate.orderStatusText = this.getKeyByValue(
            orderStatus,
            data.Status
          )
          this.$toast.success(
            `Status updated to ${this.getKeyByValue(
              orderStatus,
              data.Status
            )} successfully`
          )
        },
        error => {
          console.error(error)
          this.$toast.error('Order status update failed')
        }
      )
      this.closeDialogStatus()
    },
    changeStatus(item) {
      this.selectedIndex = this.ordersData.indexOf(item)
      this.selectedItem = item
      this.statusSelected = this.selectedItem.orderStatus
      this.dialogStatus = true
    },
    closeDialogStatus() {
      this.dialogStatus = false
      this.$nextTick(() => {
        this.selectedItem = {}
        this.selectedIndex = -1
      })
    }
  },
  computed: {
    isStatusBtnDisabled() {
      return this.statusSelected === 2
    }
  },
  created() {
    this.getAllOrders()
  },
  components: { AddAmount, ViewOrderInvoices }
}
</script>

<style lang="scss" scoped>
.manage-orders {
  margin-top: 30px;
}
</style>
