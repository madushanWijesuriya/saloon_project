<template>
  <v-card v-if="order" width="385" class="mx-4 appointment pointer">
    <ShowInvoice
      v-if="invoices"
      :invoices="invoices"
      ref="invoiceRefModel"
    ></ShowInvoice>
    <v-img
      height="120px"
      src="https://images.pexels.com/photos/1257894/pexels-photo-1257894.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=750&w=1260"
    >
      <v-card-title class="white--text mt-8">
        <p class="ml-3">Order {{ order.createdDateF }}</p>
      </v-card-title>
    </v-img>
    <v-card-title class="white--text mt-8">
      <p class="ml-3">Status {{ order.statusText }}</p>
    </v-card-title>
    <v-card-text>
      <div class="font-weight-bold ml-8 mb-2">Timeline</div>
      <v-timeline align-top dense>
        <v-timeline-item
          v-for="(status, index) in this.orderStatusList"
          :key="index"
          :color="setColor(status)"
          smallen
        >
          <div>
            <div class="font-weight-normal">
              <strong>{{ status.key }}</strong>
            </div>
          </div>
        </v-timeline-item>
      </v-timeline>
    </v-card-text>
    <v-row v-if="order.orderStatus == 2" justify="end" class="mb-10">
      <v-col md="6" lg="6" class="mt-5">
        <v-btn @click="viewInvoice" depressed color="primary">
          View Invoice
        </v-btn>
      </v-col>
    </v-row>
  </v-card>
</template>
<script>
import { orderStatus } from '../../assets/js/enums/orderEnum'
import order from '@/assets/js/api/order'
import invoices from '../../assets/js/api/invoices'
import ShowInvoice from '../../views/Order/models/ShowInvoice.vue'
import moment from 'moment'
export default {
  name: 'Order',
  mixins: [invoices, order],
  props: {
    order: { type: Object, required: true, default: () => undefined }
  },
  components: { ShowInvoice },
  data: () => ({
    orderStatusList: [],
    invoices: null
  }),
  methods: {
    createOrderStatusList() {
      for (const [key, value] of Object.entries(orderStatus)) {
        this.orderStatusList.push({ key, value })
      }
    },
    setColor(status) {
      return status.value === this.order.orderStatus ? 'green' : '#DAA520'
    },
    async viewInvoice() {
      this.loading = true
      await this.GetInvoicesForOrder(
        {
          url: '/Invoice/getAllInvoiceForProductOrder/' + this.order.id,
          Method: 'GET'
        },
        response => {
          this.invoices = response.data.map(a => {
            return {
              ...a,
              createdDateFormatted: moment(
                new Date(Date.parse(a.createdDate))
              ).format('MMM DD YYYY')
            }
          })
          this.loading = false
          setTimeout(() => {
            this.$refs.invoiceRefModel.openModal()
          }, 100)
        },
        error => {
          this.loading = true
          console.error(error)
        }
      )
    }
  },
  computed: {},
  created() {
    this.createOrderStatusList()
  }
}
</script>
<style lang="scss" scoped>
.appointment {
  position: relative;
  bottom: 0;
  transition: all ease 0.5s !important;
}
.appointment:hover {
  bottom: 10px;
  box-shadow: 0 8px 16px 0 rgba(0, 0, 0, 0.2) !important;
}
.pointer {
  cursor: pointer;
}
</style>
