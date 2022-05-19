<template>
  <v-dialog width="1000" v-model="dialog" class="view-appointment-invoices">
    <template v-slot:default="dialog">
      <v-card v-if="invoiceData">
        <v-toolbar color="primary" dark>Appointment Invoice</v-toolbar>
        <v-card-text>
          <div class="mx-6 my-6">
            <v-card class="mt-10">
              <v-row class="mx-4">
                <v-col> <h3>Invoice No</h3> </v-col>
                <v-col> {{ invoiceData.invoiceNo }} </v-col>
              </v-row>
              <v-row class="mx-4">
                <v-col> <h3>Created Date</h3> </v-col>
                <v-col>
                  {{ invoiceData.createdDateFormatted }}
                </v-col>
              </v-row>
              <v-row class="mx-4">
                <v-col> <h3>Amount</h3> </v-col>
                <v-col> LKR: {{ invoiceData.amount }} </v-col>
              </v-row>
            </v-card>
          </div>
        </v-card-text>
        <v-card-actions class="justify-end">
          <v-btn text @click="dialog.value = false">Close</v-btn>
        </v-card-actions>
      </v-card>
    </template>
  </v-dialog>
</template>

<script>
import moment from 'moment'
import appointments from '@/assets/js/api/appointments'
export default {
  name: 'ViewAppointmentInvoice',
  mixins: [appointments],
  props: {
    appointment: {
      type: Object,
      required: true,
      default: () => undefined
    }
  },
  data: () => ({
    dialog: false,
    invoiceData: null
  }),
  methods: {
    openModal() {
      this.getAppoinmentInvoices()
      this.dialog = true
    },
    async getAppoinmentInvoices() {
      const appointmentSelected = this.appointment
      const self = this
      await this.GetInvoicesForAppointment(
        {
          url: `/Invoice/appointment/${appointmentSelected.id}`,
          method: 'GET'
        },
        response => {
          const temp = response.data
          temp.createdDateFormatted = moment(
            new Date(Date.parse(response.data.createdDate))
          ).format('MMM DD YYYY')
          self.invoiceData = temp
        },
        error => {
          console.error(error)
        }
      )
    },
    closeModal() {
      this.dialog = false
      this.$nextTick(() => {
        this.appointment = null
      })
    }
  }
}
</script>

<style lang="scss" scoped></style>
