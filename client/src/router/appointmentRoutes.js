export const appointmentRoutes = [
  {
    path: '/appointments',
    name: 'appointments',
    component: () =>
      import(
        /*webpackChunkName: "appointments" */ '../views/Appointments/Appointment.vue'
      ),
    meta: {
      isRequiredAuth: true
    }
  }
]
