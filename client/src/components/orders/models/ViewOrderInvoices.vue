<template>
  <v-dialog width="1000" v-model="dialog" class="view-order-invoices">
    <template v-slot:default="dialog">
      <v-card>
        <v-toolbar color="primary" dark>Product Invoices</v-toolbar>
        <v-card-text>
          <div class="mx-6 my-6">
            <v-card
              class="mt-10"
              v-for="(inv, index) in invoicesData"
              :key="index"
            >
              <v-row class="mx-4">
                <v-col> <h3>Invoice No</h3> </v-col>
                <v-col> {{ inv.invoiceNo }} </v-col>
              </v-row>
              <v-row class="mx-4">
                <v-col> <h3>Created Date</h3> </v-col>
                <v-col>
                  {{ inv.createdDateFormatted }}
                </v-col>
              </v-row>
              <v-row class="mx-4">
                <v-col> <h3>Amount</h3> </v-col>
                <v-col> LKR: {{ inv.amount }} </v-col>
              </v-row>
            </v-card>
          </div>
        </v-card-text>
        <v-card-actions class="justify-end">
          <v-btn text @click="dialog.value = false">Close</v-btn>
        </v-card-actions>
      </v-card>
    </template>
  </v-dialog>
</template>

<script>
import order from '@/assets/js/api/order'
import moment from 'moment'
export default {
  name: 'ViewOrderInvoices',
  mixins: [order],
  props: {
    order: {
      type: Object,
      required: true,
      default: () => undefined
    }
  },
  data: () => ({
    dialog: false,
    invoicesData: []
  }),
  methods: {
    openModal() {
      this.getInvoices()
      this.dialog = true
    },
    async getInvoices() {
      const orderSelected = this.order
      const self = this
      await this.GetInvoicesForOrder(
        {
          url: `/Invoice/getAllInvoiceForProductOrder/${orderSelected.id}`,
          method: 'GET'
        },
        response => {
          self.invoicesData = response.data.map(a => {
            return {
              ...a,
              createdDateFormatted: moment(
                new Date(Date.parse(a.createdDate))
              ).format('MMM DD YYYY')
            }
          })
        },
        error => {
          console.error(error)
        }
      )
    },
    closeModal() {
      this.dialog = false
      this.$nextTick(() => {
        this.order = null
      })
    }
  }
}
</script>

<style lang="scss" scoped></style>
