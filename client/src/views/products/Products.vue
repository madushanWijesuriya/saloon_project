<template>
  <div class="products">
    <AddNewProduct ref="addNewProductRef" @save-product="saveProduct" />
    <v-row>
      <v-col>
        <v-btn
          v-if="isUserAdmin"
          class="ml-3 mt-7 white--text"
          color="#56896c"
          @click="openModel"
          >Add new product</v-btn
        >
      </v-col>
      <v-col md="5">
        <v-row>
          <v-col>
            <v-text-field
              v-model="searchProduct"
              label="Search Products"
              class="mt-5 mr-3"
            ></v-text-field>
          </v-col>
          <v-col cols="3">
            <v-btn
              class="mr-3 mt-7 white--text"
              color="primary"
              @click="searchProducts"
              >Search</v-btn
            >
          </v-col>
        </v-row>
      </v-col>
    </v-row>
    <div v-if="!loading" class="product-wrapper mx-2">
      <Product
        v-for="(product, index) in products"
        :product="product"
        :key="index"
        @product-deleted="productDeleted"
        @product-updated="productUpdated"
      />
    </div>
    <div v-else class="loadingProgress">
      <v-progress-circular
        indeterminate
        size="60"
        color="#224638"
      ></v-progress-circular>
    </div>
  </div>
</template>
<script>
import product from '@/assets/js/api/product'
import Product from '../../components/products/Product.vue'
import AddNewProduct from '../../components/products/models/AddNewProduct.vue'
export default {
  name: 'Products',
  components: { Product, AddNewProduct },
  mixins: [product],
  data: () => ({
    products: [],
    loading: false,
    s: '',
    showAddNewModal: false,
    searchProduct: ''
  }),
  methods: {
    productUpdated(product) {
      const selectedProduct = this.products.find(p => p.id === product.Id)
      selectedProduct.name = product.Name
      selectedProduct.description = product.Description
      selectedProduct.price = product.Price
      selectedProduct.quentity = product.Quentity
    },
    productDeleted(productId) {
      this.products = this.products.filter(p => p.id !== productId)
    },
    async getAllProducts() {
      this.loading = true
      const data = {
        method: 'GET',
        url: '/Products'
      }
      await this.getProducts(
        data,
        response => {
          this.loading = false
          this.products = response.data.map(o => {
            return {
              ...o,
              isShow: true
            }
          })
        },
        error => {
          this.loading = false
          console.error(error)
        }
      )
    },
    openModel() {
      this.$refs.addNewProductRef.openModel()
    },
    closeModel() {
      this.$refs.addNewProductRef.closeModel()
    },
    async saveProduct(product) {
      this.$refs.addNewProductRef.startLoading()
      const formData = new FormData()
      formData.append('Name', product.Name)
      formData.append('Description', product.Description)
      formData.append('Price', product.Price)
      formData.append('Quentity', product.Quentity)
      product.ProductImages.forEach(p => formData.append('ProductImages', p))

      const data = {
        url: '/Products',
        method: 'POST',
        data: formData,
        headers: { 'Content-Type': 'multipart/form-data' }
      }

      await this.postProduct(
        data,
        response => {
          response.data.isShow = true
          this.products.push(response.data)
          this.$refs.addNewProductRef.stopLoading()
          this.closeModel()
          this.$toast.success('Product created successfully')
        },
        error => {
          this.$refs.addNewProductRef.stopLoading()
          for (const [key, value] of Object.entries(
            error.response.data.errors
          )) {
            console.log(key)
            value.forEach(v => {
              this.$toast.error(v)
            })
          }
          console.error(error)
        }
      )
    },
    searchProducts() {
      const query = this.searchProduct.toString().toLowerCase()
      const filteredItems = this.products.filter(
        p => p.name.toLowerCase().indexOf(query) >= 0
      )
      this.products.forEach(p => {
        if (filteredItems.some(fi => fi.id === p.id)) {
          p.isShow = true
        } else {
          p.isShow = false
        }
      })
    }
  },
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
  async created() {
    await this.getAllProducts()
  }
}
</script>

<style lang="scss" scoped>
@import '@/assets/styles/variables.scss';
.product-wrapper {
  display: flex;
  flex-wrap: wrap;
  justify-content: flex-start;
}
</style>
