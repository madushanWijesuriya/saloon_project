<template>
  <v-card
    v-if="product"
    :loading="loading"
    :class="['product my-7 mx-3']"
    max-width="287"
    v-show="product.isShow"
  >
    <template slot="progress">
      <v-progress-linear
        color="deep-purple"
        height="10"
        indeterminate
      ></v-progress-linear>
    </template>
    <span
      @click="isUserAdmin && editProduct()"
      :class="[isUserAdmin ? 'pointer' : '']"
    >
      <v-img height="250" :src="setImage(product.productImages)"></v-img>
      <v-card-title>{{ product.name }}</v-card-title>
      <v-card-text>
        <Like :product="product" />
        <div class="my-4 overline">
          <b>LKR {{ product.price }}</b>
        </div>
        <div>
          {{ product.description }}
        </div>
      </v-card-text>
      <v-divider class="mx-4"></v-divider>
    </span>
    <DeleteConfirmation
      ref="deleteConfirmationRef"
      :product="product"
      @delete-product="deleteProductConfirm"
      @delete-reject="deleteReject"
    />
    <EditProduct
      ref="editProductRef"
      v-if="product"
      :product="product"
      @update-product="updateProduct"
    />
    <ProductDetails v-if="product" ref="productDetailsRef" :product="product" />
    <v-card-actions>
      <v-btn v-if="isUserAdmin" color="error" outlined @click="deleteProduct">
        Delete
      </v-btn>
      <v-btn color="primary" outlined @click="viewProductDetails"> View </v-btn>
      <v-btn color="success" outlined @click="buyProduct"> To Cart </v-btn>
    </v-card-actions>
  </v-card>
</template>
<script>
import Like from './Like.vue'
import DeleteConfirmation from './models/DeleteConfirmation.vue'
import product from '@/assets/js/api/product'
import EditProduct from './models/EditProduct.vue'
import ProductDetails from './models/ProductDetails.vue'
export default {
  name: 'product',
  mixins: [product],
  props: {
    product: {
      type: Object,
      required: true,
      default: () => undefined
    }
  },
  data: () => ({
    loading: false,
    selection: 1
  }),
  methods: {
    setImage(images) {
      if (images.length < 1) {
        return 'https://images.pexels.com/photos/234220/pexels-photo-234220.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=750&w=1260'
      }

      return require(`../../../../saloonAPI/saloonAPI/Images/${images[0].uniqueName}`)
    },
    buyProduct() {
      this.$store.dispatch('addToCart', this.product)
    },
    editProduct() {
      this.$refs.editProductRef.openModal()
    },
    deleteReject() {
      this.loading = false
    },
    deleteProduct() {
      this.loading = true
      this.$refs.deleteConfirmationRef.openModal()
    },
    async deleteProductConfirm(productId) {
      this.deleteProductFromDB(
        {
          url: `/Products/${productId}`,
          method: 'DELETE'
        },
        () => {
          this.loading = false
          this.$emit('product-deleted', productId)
        },
        error => {
          console.error(error)
          this.$toast.error('product delete failed please contact support')
        }
      )
      this.$refs.deleteConfirmationRef.closeModal()
    },
    updateProduct(product) {
      this.updateProductRequest(
        {
          url: `/Products/${product.Id}`,
          method: 'PUT',
          data: product
        },
        () => {
          this.loading = false
          this.$emit('product-updated', product)
        },
        error => {
          console.error(error)
          this.$toast.error('product update failed please contact support')
        }
      )
      this.$refs.editProductRef.closeModal()
    },
    viewProductDetails() {
      this.$refs.productDetailsRef.openModal()
    }
  },
  mounted() {},
  computed: {
    user() {
      return this.$store.state.user.user
    },
    isUserAdmin() {
      return this.user && this.user.userRoles
        ? this.user.userRoles.some(role => role === 'Admin')
        : false
    }
  },
  components: { Like, DeleteConfirmation, EditProduct, ProductDetails }
}
</script>
<style lang="scss">
.product {
  position: relative;
  bottom: 0;
  transition: all ease 0.5s !important;
}
.product:hover {
  bottom: 10px;
  box-shadow: 0 8px 16px 0 rgba(0, 0, 0, 0.2) !important;
}
.pointer {
  cursor: pointer;
}
</style>
