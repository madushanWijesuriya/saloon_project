<template>
  <div class="appointments">
    <v-card>
      <v-tabs color="#897a58" right v-model="tab">
        <v-tab :disabled="!isUserAdmin" class="v-tab"
          >Manage Appointments</v-tab
        >
        <v-tab class="v-tab">My appointments</v-tab>
        <v-tab class="v-tab">Place Appointment</v-tab>
        <v-tab-item v-for="n in 3" :key="n">
          <v-container fluid class="mt-3">
            <ManageAppointments v-if="n === 1" />
            <MyAppointments ref="myAppointmentsRef" v-if="n === 2" />
            <PlaceAppointments
              @new-appointment-created="onNewAppointmentCreated"
              v-if="n === 3"
            />
          </v-container>
        </v-tab-item>
      </v-tabs>
    </v-card>
  </div>
</template>
<script>
import ManageAppointments from '../../components/appointments/ManageAppointments.vue'
import MyAppointments from '../../components/appointments/MyAppointments.vue'
import PlaceAppointments from '../../components/appointments/PlaceAppointments.vue'
export default {
  data() {
    return {
      tab: 1,
      items: [
        { title: 'Dashboard', icon: 'mdi-view-dashboard' },
        { title: 'Account', icon: 'mdi-account-box' },
        { title: 'Admin', icon: 'mdi-gavel' }
      ],
      drawer: true
    }
  },
  methods: {
    onNewAppointmentCreated() {
      this.tab = 1
    }
  },
  computed: {
    user() {
      return this.$store.state.user.user
    },
    isUserAdmin() {
      return this.user && this.user.userRoles
        ? this.user.userRoles.some(role => role === 'Admin')
        : false
    }
  },
  components: { ManageAppointments, MyAppointments, PlaceAppointments }
}
</script>
<style lang="scss">
.appointments {
  margin: 0;
  padding: 0;
}
</style>
