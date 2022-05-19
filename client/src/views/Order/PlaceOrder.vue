<template>
  <div class="place-order">
    <v-row class="mt-8 ml-4">
      <v-col cols="4">
        <v-card min-height="250">
          <v-row>
            <v-col cols="5">
              <h3 class="pt-6 pl-4">Items Count:</h3>
            </v-col>
            <v-col cols="7">
              <h3 class="pt-6 pl-4">{{ cartCount }}</h3>
            </v-col>
          </v-row>
          <v-row>
            <v-col cols="5">
              <h3 class="pl-4">Discount:</h3>
            </v-col>
            <v-col cols="7">
              <h3 class="pl-4">LKR: 0</h3>
            </v-col>
          </v-row>
          <v-row>
            <v-col cols="5">
              <h3 class="pl-4">Delivery Fee:</h3>
            </v-col>
            <v-col cols="7">
              <h3 class="pl-4">LKR: {{ deliveryFee }}</h3>
            </v-col>
          </v-row>
          <v-row>
            <v-col cols="5">
              <h3 class="pl-4">Total Amount:</h3>
            </v-col>
            <v-col cols="7">
              <h3 class="pl-4">LKR: {{ totalAmount + deliveryFee }}</h3>
            </v-col>
          </v-row>
        </v-card>
      </v-col>
      <v-col>
        <v-card min-height="230" class="mr-5">
          <v-row>
            <v-col cols="6">
              <h4 class="pb-3 mt-2 pl-8">Delivery Address</h4>
              <v-textarea
                class="pl-4"
                solo
                name="input-7-4"
                label="Enter Delivery Address"
                v-model="address"
              ></v-textarea>
            </v-col>
            <v-col cols="6">
              <h4 class="pb-3 mt-2 pl-7">Mobile Number</h4>
              <v-text-field
                class="ml-6 mr-4"
                name="input-7-4"
                label="Enter Mobile Number"
                outlined
                v-model="mobileNo"
              ></v-text-field>
            </v-col>
          </v-row>
        </v-card>
      </v-col>
    </v-row>
    <v-row>
      <v-col>
        <v-card min-width="200" class="mx-6">
          <div class="ml-6 py-6">
            <v-btn class="white--text mr-5" color="green" @click="placeTheOrder"
              >Place Order</v-btn
            >
            <v-btn class="" color="gray" @click="$router.push('/cart')"
              >Back To Cart</v-btn
            >
          </div>
        </v-card>
      </v-col>
    </v-row>
  </div>
</template>
<script>
import order from '@/assets/js/api/order'

export default {
  name: 'PlaceOrder',
  mixins: [order],
  data: () => ({
    mobileNo: '',
    address: ''
  }),
  methods: {
    placeTheOrder() {
      let error = false
      if (!this.address) {
        this.$toast.error('Please enter delivery address')
        error = true
      }
      if (!this.mobileNo) {
        this.$toast.error('Please enter mobile no')
        error = true
      }
      if (this.mobileNo && !Number.isInteger(parseInt(this.mobileNo))) {
        this.$toast.error('Mobile number should be numeric')
        error = true
      }
      if (error) {
        return
      }

      this.placeOrder(
        {
          url: '/Orders',
          method: 'POST',
          data: {
            Products: this.cartData,
            MobileNo: this.mobileNo,
            DeliveryAddress: this.address
          }
        },
        () => {
          this.$toast.success(
            'Order placed successfully, make the payment and contact us to proceed'
          )
          this.$store.dispatch('clearAllFromCart')
          setTimeout(() => {
            this.$router.push('/')
          }, 4000)
        },
        error => {
          console.error(error)
        }
      )
    }
  },
  computed: {
    cartCount() {
      return this.$store.getters.getCartCount
    },
    cartData() {
      return this.$store.getters.getCartData
    },
    totalAmount() {
      return this.cartData
        ? this.cartData.reduce((sum, { price }) => {
            return sum + price
          }, 0)
        : 0
    },
    deliveryFee() {
      return this.cartCount && this.cartCount > 0 ? 200 : 0
    }
  }
}
</script>
<style lang="scss"></style>
