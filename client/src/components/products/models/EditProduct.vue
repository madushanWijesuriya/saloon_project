<template>
  <v-dialog transition="dialog-top-transition" max-width="800" v-model="dialog">
    <template v-slot:default="dialog">
      <v-card>
        <v-toolbar color="primary" dark>Edit Product</v-toolbar>
        <v-card-text>
          <v-form class="mt-5" ref="form" v-model="valid" lazy-validation>
            <v-text-field
              v-model="name"
              :counter="100"
              :rules="nameRules"
              label="Name"
              required
            ></v-text-field>

            <v-text-field
              v-model="quentity"
              :rules="quentityRules"
              label="Quentity"
              required
            ></v-text-field>

            <v-text-field
              v-model="price"
              :rules="priceRules"
              label="Price"
              required
            ></v-text-field>

            <v-textarea
              :rules="descriptionRules"
              name="description"
              label="Description"
              :value="description"
              v-model="description"
              hint="Discriber product"
              :counter="100"
            />

            <v-btn
              :disabled="!valid"
              color="#56896c"
              class="mr-4 mt-5 white--text"
              @click="validate"
              :loading="updateLoading"
            >
              Update Product
            </v-btn>

            <v-btn
              class="mt-5"
              color="gray"
              @click="handler(resetValidation, reset)"
            >
              Clear
            </v-btn>
          </v-form>
        </v-card-text>
        <v-card-actions class="justify-end">
          <v-btn text @click="dialog.value = false">Close</v-btn>
        </v-card-actions>
      </v-card>
    </template>
  </v-dialog>
</template>
<script>
export default {
  data: () => ({
    dialog: false,
    updateLoading: false,
    quentity: 1,
    price: null,
    valid: true,
    name: '',
    nameRules: [
      v => !!v || 'Name is required',
      v => (v && v.length <= 100) || 'Name must be less than 100 characters'
    ],
    description: '',
    descriptionRules: [],
    quentityRules: [],
    priceRules: [],
    productImages: []
  }),
  props: {
    product: {
      type: Object,
      required: true,
      default: () => undefined
    }
  },
  methods: {
    handler(arg1, arg2) {
      arg1()
      arg2()
    },
    openModal() {
      this.dialog = true
      this.$nextTick(() => {
        this.name = this.product.name
        this.description = this.product.description
        this.quentity = this.product.quentity
        this.price = this.product.price
        this.productImages = this.product.productImages
      })
    },
    closeModal() {
      this.resetValidation()
      this.reset()
      this.$nextTick(() => {
        this.dialog = false
      })
    },
    startLoading() {
      this.updateLoading = true
    },
    stopLoading() {
      this.updateLoading = false
    },
    validate() {
      this.$refs.form.validate()
      if (this.valid) this.updateProduct()
    },
    reset() {
      this.$refs.form.reset()
      this.stopLoading()
    },
    resetValidation() {
      this.$refs.form.resetValidation()
    },
    setFiles(files) {
      this.fileLoading = true
      this.productImages = files
      this.fileLoading = false
    },
    updateProduct() {
      let error = false
      if (!this.price) {
        this.$toast.error('please enter a valid price')
        error = true
      }
      if (!this.quentity) {
        this.$toast.error('please enter a valid quentity')
        error = true
      }
      if (!this.name) {
        this.$toast.error('please enter a valid name')
        error = true
      }
      if (!Number.isInteger(this.quentity)) {
        this.$toast.error('quentity should be number')
        error = true
      }
      if (this.quentity > 100000) {
        this.$toast.error('quentity should be less than 100000')
        error = true
      }
      if (Number.isNaN(this.price)) {
        this.$toast.error('price should be numaric')
        error = true
      }
      if (
        this.description &&
        this.description.length > 0 &&
        typeof this.description !== 'string' &&
        typeof this.description !== 'number'
      ) {
        this.$toast.error('invalid description')
        error = true
      }
      if (error) return
      const product = {
        Id: this.product.id,
        Name: this.name,
        Quentity: this.quentity,
        Price: this.price,
        Description: this.description
        // ProductImages: this.productImages
      }
      this.$emit('update-product', product)
    },
    mounted() {}
  }
}
</script>
<style lang="scss"></style>
