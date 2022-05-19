<template>
  <div class="">
    <v-card class="py-6">
      <h1 class="my-6 mx-6">Cart</h1>
      <div class="cart-items mx-10" v-if="cartData.length > 0">
        <div
          class="cart-item my-6"
          v-for="(item, index) in cartData"
          :key="index"
        >
          <v-row align="center">
            <v-col cols="2">
              <v-img
                width="120"
                :src="getImageSrc(item.productImages)"
                alt="product-image"
              ></v-img>
            </v-col>
            <v-col cols="2">
              <h3>{{ item.name }}</h3>
            </v-col>
            <v-col cols="2">
              <h4>x &nbsp;&nbsp;&nbsp; 1</h4>
            </v-col>
            <v-col cols="2">
              <h4>LKR. {{ item.price }}</h4>
            </v-col>
            <v-col cols="2">
              <v-btn text icon color="red" @click="removeFromCart(item)">
                <v-icon>mdi-delete</v-icon>
              </v-btn>
            </v-col>
          </v-row>
        </div>
        <v-row>
          <div class="mt-6 ml-3">
            <v-btn color="green" class="white--text" @click="placeOrder">
              Place Order
            </v-btn>
            <v-btn color="red" class="ml-5 white--text" @click="clearAll">
              Clear All
            </v-btn>
          </div>
        </v-row>
      </div>
      <div v-else>
        <h2 class="mx-6">No products added to cart yet</h2>
      </div>
    </v-card>
  </div>
</template>
<script>
export default {
  name: 'Cart',
  data: () => ({}),
  methods: {
    getImageSrc(images) {
      if (images.length < 1) {
        return 'https://images.pexels.com/photos/234220/pexels-photo-234220.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=750&w=1260'
      }
      return require(`../../../../saloonAPI/saloonAPI/Images/${images[0].uniqueName}`)
    },
    clearAll() {
      this.$store.dispatch('clearAllFromCart')
    },
    removeFromCart(product) {
      this.$store.dispatch('removeFromCart', product)
    },
    placeOrder() {
      this.$router.push('/placeOrder')
    }
  },
  computed: {
    cartData() {
      return this.$store.getters.getCartData
    }
  }
}
</script>
<style lang="scss" scoped>
.cart-item {
  border: 1px solid lightblue;
  padding: 5px;
  border-radius: 8px;
  position: relative;
  transition: all ease 0.5s !important;
}
.cart-item:hover {
  bottom: 2px;
  box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2) !important;
}
</style>
