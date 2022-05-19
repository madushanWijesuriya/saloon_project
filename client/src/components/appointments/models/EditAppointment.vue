<template>
  <v-dialog transition="dialog-top-transition" max-width="800" v-model="dialog">
    <template v-slot:default="dialog">
      <v-card>
        <v-toolbar color="primary" dark>Edit Appointment</v-toolbar>
        <v-card-text>
          <v-form class="mt-5" ref="form" v-model="valid" lazy-validation>
            <v-text-field
              v-model="DurationInMins"
              :counter="100"
              :rules="DurationRules"
              label="DurationRules"
              required
            ></v-text-field>
            <v-menu
              v-model="menu2"
              :close-on-content-click="false"
              :nudge-right="40"
              transition="scale-transition"
              offset-y
              min-width="auto"
            >
              <template v-slot:activator="{ on, attrs }">
                <v-text-field
                  v-model="date"
                  label="Choose appointment date"
                  prepend-icon="mdi-calendar"
                  readonly
                  v-bind="attrs"
                  v-on="on"
                ></v-text-field>
              </template>
              <v-date-picker
                v-model="date"
                @input="menu2 = false"
              ></v-date-picker>
            </v-menu>
            <v-select
              v-model="type"
              :items="items"
              label="Appointment Type"
            ></v-select>

            <v-btn
              :disabled="!valid"
              color="#56896c"
              class="mr-4 mt-5 white--text"
              @click="validate"
              :loading="updateLoading"
            >
              Update Appointment
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
import { appointmentType } from '@/assets/js/enums/appointmentEnum'
import objectHelper from '@/assets/js/healpers/objectHelper'
import moment from 'moment'
export default {
  mixins: [objectHelper],
  data: () => ({
    dialog: false,
    updateLoading: false,
    quentity: 1,
    price: null,
    valid: true,
    DurationInMins: '',
    type: '',
    DurationRules: [v => !!v || 'Duration time is required'],
    items: [
      'HairCut',
      'ColouringAndStyling',
      'NailTreatment',
      'FacialAndSkinCareTreatment',
      'WaxingAndHairRemoval',
      'Makeup',
      'Others'
    ],
    date: new Date(Date.now() - new Date().getTimezoneOffset() * 60000)
      .toISOString()
      .substr(0, 10),
    menu2: false
  }),
  props: {
    appointment: {
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
        this.type = this.getKeyByValue(appointmentType, this.appointment.type)
        this.DurationInMins = this.appointment.durationInMins
        this.date = moment(this.appointment.appoinmentDate).format('yyyy-MM-DD')
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
      if (this.valid) this.updateAppointment()
    },
    reset() {
      this.$refs.form.reset()
      this.stopLoading()
    },
    resetValidation() {
      this.$refs.form.resetValidation()
    },

    updateAppointment() {
      let error = false

      if (!this.DurationInMins) {
        this.$toast.error('please enter a valid time')
        error = true
      }
      if (this.DurationInMins.length > 3) {
        this.$toast.error('this must be less than 2 hours')
        error = true
      }
      if (!this.date) {
        this.$toast.error('please enter a valid date')
        error = true
      }
      if (!this.type) {
        this.$toast.error('please select a valid type')
        error = true
      }
      if (!Number.isInteger(parseInt(this.DurationInMins))) {
        this.$toast.error('duration should be number')
        error = true
      }

      if (error) return
      const appointment = {
        Type: appointmentType[this.type],
        DurationInMins: this.DurationInMins,
        AppoinmentDate: this.date,
        Id: this.appointment.id
      }

      this.$emit('update-appointment', appointment)
    },
    mounted() {}
  }
}
</script>
<style lang="scss"></style>
