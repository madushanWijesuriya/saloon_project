<template>
  <v-dialog v-if="product" v-model="dialog" persistent max-width="400">
    <v-card>
      <v-card-title class="text-h5"> Delete confirmation? </v-card-title>
      <v-card-text :style="{ fontSize: '16px' }"
        >Are you sure want to delete this product, this can not be reverted once
        done ?
      </v-card-text>
      <v-card-actions class="pb-4">
        <v-spacer></v-spacer>
        <v-btn color="error" @click="deleteProduct"> Delete </v-btn>
        <v-btn color="gray" class="ml-3" text @click="closeModal">
          Cancel
        </v-btn>
      </v-card-actions>
    </v-card>
  </v-dialog>
</template>
<script>
export default {
  name: 'DeleteConfirmation',
  data: () => ({
    dialog: false
  }),
  props: {
    product: {
      type: Object,
      required: true,
      default: () => undefined
    }
  },
  methods: {
    openModal() {
      this.dialog = true
    },
    closeModal() {
      this.dialog = false
      this.$emit('delete-reject')
    },
    deleteProduct() {
      this.$emit('delete-product', this.product.id)
    }
  }
}
</script>
<style lang=""></style>
