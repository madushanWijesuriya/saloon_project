<template>
  <v-dialog
    transition="dialog-top-transition"
    max-width="800"
    v-model="addNewDialog"
  >
    <template v-slot:default="dialog">
      <v-card>
        <v-toolbar color="primary" dark>Add Product</v-toolbar>
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
            <v-file-input
              show-size
              counter
              multiple
              label="Product Images"
              fileLoading
              @change="setFiles"
              prepend-icon="mdi-camera"
              accept="image/*"
            ></v-file-input>

            <v-btn
              :disabled="!valid"
              color="#56896c"
              class="mr-4 mt-5 white--text"
              @click="validate"
              :loading="saveLoading"
            >
              Save Product
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
  name: 'AddNewProduct',
  props: {},
  data: () => ({
    saveLoading: false,
    fileLoading: false,
    addNewDialog: false,
    quentity: 1,
    price: null,
    valid: true,
    productImages: [],
    name: '',
    nameRules: [
      v => !!v || 'Name is required',
      v => (v && v.length <= 100) || 'Name must be less than 100 characters'
    ],
    description: '',
    descriptionRules: [],
    quentityRules: [],
    priceRules: []
  }),
  methods: {
    handler(arg1, arg2) {
      arg1()
      arg2()
    },
    openModel() {
      this.addNewDialog = true
    },
    closeModel() {
      this.resetValidation()
      this.reset()
      this.$nextTick(() => {
        this.addNewDialog = false
      })
    },
    startLoading() {
      this.saveLoading = true
    },
    stopLoading() {
      this.saveLoading = false
    },
    validate() {
      this.$refs.form.validate()
      if (this.valid) this.saveProduct()
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
    saveProduct() {
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
      if (Number.isNaN(parseInt(this.quentity))) {
        this.$toast.error('quentity should be number')
        error = true
      }
      if (this.quentity > 100000) {
        this.$toast.error('quentity should be less than 100000')
        error = true
      }
      if (Number.isNaN(parseFloat(this.price))) {
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
        Name: this.name,
        Quentity: this.quentity,
        Price: this.price,
        Description: this.description,
        ProductImages: this.productImages
      }
      this.$emit('save-product', product)
    }
  },
  computed: {}
}
</script>
<style lang="scss">
@import '@/assets/styles/variables.scss';
</style>
