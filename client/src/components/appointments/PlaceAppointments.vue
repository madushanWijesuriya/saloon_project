<template>
  <v-card>
    <v-toolbar color="primary" dark>Place Appointment</v-toolbar>
    <v-card-text>
      <v-form class="mt-5" ref="form" v-model="valid" lazy-validation>
        <v-text-field
          v-model="DurationInMins"
          :counter="3"
          :rules="DurationRules"
          label="Duration"
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
          <v-date-picker v-model="date" @input="menu2 = false"></v-date-picker>
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
          :loading="saveLoading"
        >
          Save Appoinment
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
<script>
import appointments from '@/assets/js/api/appointments'
import { appointmentType } from '@/assets/js/enums/appointmentEnum'
import objectHelper from '../../assets/js/healpers/objectHelper'
export default {
  name: 'PlaceAppointment',
  mixins: [appointments, objectHelper],
  props: {},
  data: () => ({
    saveLoading: false,
    fileLoading: false,
    addNewDialog: false,
    valid: true,
    DurationInMins: '',
    type: null,
    DurationRules: [
      v => !!v || 'Duration time is required',
      v => (v && v.length <= 3) || 'Duration must be less than 2 hours'
    ],
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
  methods: {
    handler(arg1, arg2) {
      arg1()
      arg2()
    },
    resetAllFormData() {
      this.resetValidation()
      this.reset()
    },
    startLoading() {
      this.saveLoading = true
    },
    stopLoading() {
      this.saveLoading = false
    },
    validate() {
      this.$refs.form.validate()
      if (this.valid) this.saveAppointment()
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
    async saveAppointment() {
      let error = false

      if (!this.DurationInMins) {
        this.$toast.error('please enter a valid time')
        error = true
      }
      if (!this.date) {
        this.$toast.error('please enter a valid date')
        error = true
      }
      console.log(this.type)
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
        AppoinmentDate: this.date
      }

      this.startLoading()

      const data = {
        url: '/Appointment',
        method: 'POST',
        data: appointment
      }

      await this.postAppointment(
        data,
        () => {
          this.stopLoading()
          this.resetAllFormData()
          this.$toast.success('Appointment created successfully')
          setTimeout(() => {
            this.$emit('new-appointment-created')
          }, 100)
        },
        error => {
          this.stopLoading()
          if (
            error &&
            error.response &&
            error.response.data &&
            error.response.data.errors
          ) {
            for (const [key, value] of Object.entries(
              error.response.data.errors
            )) {
              console.log(key)
              value.forEach(v => {
                this.$toast.error(v)
              })
            }
          }
          console.error(error)
        }
      )
    }
  },
  computed: {}
}
</script>
<style lang="scss">
@import '@/assets/styles/variables.scss';
</style>
