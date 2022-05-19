<template>
  <v-dialog width="1000" v-model="dialog" class="add-amount">
    <template v-slot:default="dialog">
      <v-card>
        <v-toolbar color="primary" dark>Add Amount</v-toolbar>
        <v-card-text>
          <div class="mx-6 mt-8">
            <v-text-field v-model="amount" label="Amount"></v-text-field>
          </div>
        </v-card-text>
        <v-card-actions class="justify-end pb-5">
          <v-btn color="green" @click="generateInvoice" class="mr-3"
            >Generate Invoice</v-btn
          >
          <v-btn class="mr-2" @click="dialog.value = false">Close</v-btn>
        </v-card-actions>
      </v-card>
    </template>
  </v-dialog>
</template>

<script>
export default {
  name: 'AddAmount',
  data: () => ({
    amount: 0,
    dialog: false
  }),
  methods: {
    openModal() {
      this.dialog = true
    },
    closeModal() {
      this.dialog = false
    },
    generateInvoice() {
      if (Number.isNaN(parseFloat(this.amount))) {
        this.$toast.error('Please enter valid amount')
        return
      }
      this.$emit('generate-invoice', this.amount)
    }
  }
}
</script>

<style lang="scss" scoped></style>
