<template>
  <v-card>
    <v-toolbar flat color="gray">
      <v-toolbar-title>Reports</v-toolbar-title>
    </v-toolbar>
    <v-tabs vertical>
      <v-tab>
        <v-icon left> mdi-account </v-icon>
        Monthly Appointments
      </v-tab>
      <v-tab>
        <v-icon left> mdi-access-point </v-icon>
        Monthly Customers &nbsp;&nbsp;&nbsp;
      </v-tab>
      <v-tab>
        <v-icon left> mdi-lock </v-icon>
        Monthly Products &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
      </v-tab>
      <v-tab-item v-for="i in 3" :key="i">
        <v-card flat>
          <v-card-text>
            <AppointmentReports v-if="i === 1" />
            <UserReports v-if="i === 2" />
            <ProductReports v-if="i === 3" />
          </v-card-text>
        </v-card>
      </v-tab-item>
    </v-tabs>
  </v-card>
</template>
<script>
import AppointmentReports from '@/components/reports/AppointmentReports.vue'
import UserReports from '@/components/reports/UserReports.vue'
import ProductReports from '@/components/reports/ProductReports.vue'
export default {
  components: { AppointmentReports, UserReports, ProductReports }
}
</script>
<style lang="scss" scoped></style>
