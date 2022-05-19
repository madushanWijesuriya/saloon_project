<template>
  <v-dialog transition="dialog-top-transition" max-width="800" v-model="dialog">
    <template v-slot:default="dialog">
      <v-card>
        <v-toolbar color="primary" dark>Invoice Details</v-toolbar>
        <v-card-text>
          <v-card v-for="(invoice, index) in invoices" :key="index">
            <div class="py-6">
              <v-row class="mx-4">
                <v-col> <h3>Invoice Id</h3> </v-col>
                <v-col> {{ invoice.id }} </v-col>
              </v-row>
              <v-row class="mx-4">
                <v-col> <h3>Invoice No</h3> </v-col>
                <v-col> {{ invoice.invoiceNo }} </v-col>
              </v-row>
              <v-row class="mx-4">
                <v-col> <h3>Invoice Date</h3> </v-col>
                <v-col> {{ invoice.createdDate }} </v-col>
              </v-row>
              <v-row class="mx-4">
                <v-col> <h3>Invoice Amount</h3> </v-col>
                <v-col>Rs. {{ invoice.amount }} </v-col>
              </v-row>
            </div>
          </v-card>
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
    dialog: false
  }),
  props: {
    invoices: {
      type: Array,
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
    }
  }
}
</script>
<style lang="scss"></style>
