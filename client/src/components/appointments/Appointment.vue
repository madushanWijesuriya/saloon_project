<template>
  <v-card
    @click="editAppointment"
    v-if="appointment"
    width="385"
    class="mx-4 appointment pointer"
  >
    <EditAppointment
      ref="editAppointmentRef"
      v-if="appointment"
      :appointment="appointment"
      @update-appointment="updateAppointment"
    />
    <v-img
      height="120px"
      src="https://images.pexels.com/photos/1257894/pexels-photo-1257894.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=750&w=1260"
    >
      <v-card-title class="white--text mt-8">
        <p class="ml-3">Appoinment {{ appointment.appoinmentDateF }}</p>
      </v-card-title>
    </v-img>

    <v-card-text>
      <div class="font-weight-bold ml-8 mb-2">Timeline</div>

      <v-timeline align-top dense>
        <v-timeline-item
          v-for="(status, index) in this.appointmentStatusList"
          :key="index"
          :color="setColor(status)"
          small
        >
          <div>
            <div class="font-weight-normal">
              <strong>{{ status.key }}</strong>
            </div>
          </div>
        </v-timeline-item>
      </v-timeline>
    </v-card-text>
  </v-card>
</template>
<script>
import { appointmentStatus } from '@/assets/js/enums/appointmentEnum'
import EditAppointment from './models/EditAppointment.vue'
import appointments from '@/assets/js/api/appointments'

import objectHelper from '@/assets/js/healpers/objectHelper'
export default {
  name: 'Appointment',
  mixins: [objectHelper, appointments],
  props: {
    appointment: { type: Object, required: true, default: () => undefined }
  },
  data: () => ({
    appointmentStatusList: []
  }),
  methods: {
    editAppointment() {
      this.$refs.editAppointmentRef.openModal()
    },
    createAppointmentStatusList() {
      for (const [key, value] of Object.entries(appointmentStatus)) {
        this.appointmentStatusList.push({ key, value })
      }
    },
    setColor(status) {
      return status.value === this.appointment.status ? 'green' : '#DAA520'
    },
    updateAppointment(appointment) {
      this.updateAppointmentRequest(
        {
          url: `/Appointment/${appointment.Id}`,
          method: 'PUT',
          data: appointment
        },
        () => {
          this.loading = false
          this.$emit('appointment-updated', appointment)
        },
        error => {
          console.error(error)
          this.$toast.error('appointment update failed please contact support')
        }
      )
      this.$refs.editAppointmentRef.closeModal()
    }
  },
  computed: {},
  created() {
    this.createAppointmentStatusList()
  },
  components: { EditAppointment }
}
</script>
<style lang="scss" scoped>
.appointment {
  position: relative;
  bottom: 0;
  transition: all ease 0.5s !important;
}
.appointment:hover {
  bottom: 10px;
  box-shadow: 0 8px 16px 0 rgba(0, 0, 0, 0.2) !important;
}
.pointer {
  cursor: pointer;
}
</style>
