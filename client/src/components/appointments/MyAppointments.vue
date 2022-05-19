<template>
  <v-row justify="start" class="mb-10">
    <div v-if="loading" class="loadingProgress">
      <v-progress-circular
        indeterminate
        size="60"
        color="#224638"
      ></v-progress-circular>
    </div>
    <div
      v-else
      class="appointment-wrapper mt-8"
      v-for="appointment in myAppointments"
      :key="appointment.id"
    >
      <Appointment
        :appointment="appointment"
        @appointment-update="appointmentUpdated"
      />
    </div>
  </v-row>
</template>
<script>
import Appointment from './Appointment.vue'
import objectHelper from '../../assets/js/healpers/objectHelper'
import appointments from '@/assets/js/api/appointments'
import moment from 'moment'
import {
  appointmentType,
  appointmentStatus
} from '@/assets/js/enums/appointmentEnum'
export default {
  name: 'MyAppointments',
  mixins: [appointments, objectHelper],
  data: () => ({
    appointments: [],
    loading: false
  }),
  methods: {
    async getAppointments() {
      this.loading = true
      await this.appointmentsGet(
        {
          url: '/Appointment',
          Method: 'GET'
        },
        response => {
          this.appointments = response.data.map(a => {
            return {
              ...a,
              appoinmentDateF: moment(
                new Date(Date.parse(a.appoinmentDate))
              ).format('MMM DD YYYY'),
              completedDateF: a.completedDate
                ? moment(new Date(Date.parse(a.completedDate))).format(
                    'MMM DD YYYY'
                  )
                : '-',
              createdDateF: moment(new Date(Date.parse(a.createdDate))).format(
                'MMM DD YYYY'
              ),
              typeText: this.getKeyByValue(appointmentType, a.type),
              statusText: this.getKeyByValue(appointmentStatus, a.status)
            }
          })
          this.loading = false
        },
        error => {
          this.loading = true
          console.error(error)
        }
      )
    },
    appointmentUpdated(appointment) {
      this.appointments
        .find(a => a.id === appointment.Id)
        .map(a => {
          return {
            ...a,
            Type: appointment.type,
            DurationInMins: appointment.DurationInMins,
            AppoinmentDate: appointment.date
          }
        })
    }
  },

  computed: {
    user() {
      return this.$store.state.user.user
    },
    myAppointments() {
      return this.appointments
        ? this.appointments.filter(a => a.userId === this.user.id)
        : []
    }
  },
  created() {
    this.getAppointments()
  },
  components: { Appointment }
}
</script>
<style lang="scss"></style>
