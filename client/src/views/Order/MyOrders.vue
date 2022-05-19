<template>
  <v-row justify="start" class="mb-10">
    <div v-if="loading" class="loadingProgress">
      <v-progress-circular
        indeterminate
        size="60"
        color="#224638"
      ></v-progress-circular>
    </div>
    <div
      v-else
      class="appointment-wrapper mt-8"
      v-for="order in orders"
      :key="order.id"
    >
      <Order :order="order" />
    </div>
  </v-row>
</template>
<script>
import Order from './Order.vue'
import orders from '../../assets/js/api/order'
import objectHelper from '../../assets/js/healpers/objectHelper'
import moment from 'moment'
import { orderStatus } from '../../assets/js/enums/orderEnum'
export default {
  components: { Order },
  name: 'MyOrders',
  mixins: [orders, objectHelper],
  data: () => ({
    orders: [],
    loading: false
  }),
  methods: {
    async getOrdersList() {
      this.loading = true
      await this.getOrders(
        {
          url: '/Orders/my-orders',
          Method: 'GET'
        },
        response => {
          this.orders = response.data.map(o => {
            return {
              ...o,
              createdDateF: moment(new Date(Date.parse(o.createdDate))).format(
                'MMM DD YYYY'
              ),
              statusText: this.getKeyByValue(orderStatus, o.orderStatus)
            }
          })
          this.loading = false
        },
        error => {
          this.loading = true
          console.error(error)
        }
      )
    }
  },
  computed: {
    user() {
      return this.$store.state.user.user
    },
    myOrders() {
      return this.orders
        ? this.orders.filter(o => o.CustomerId === this.user.id)
        : []
    }
  },
  created() {
    this.getOrdersList()
  }
}
</script>
<style lang="scss"></style>
