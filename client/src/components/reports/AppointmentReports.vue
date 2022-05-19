<template>
  <div>
    <div class="mx-4 appointment-by-type">
      <h1 class="mb-10">Appointments Type Report</h1>
      <v-row class="ml-1">
        <v-menu
          ref="menu"
          v-model="menu"
          :close-on-content-click="false"
          :return-value.sync="dates"
          transition="scale-transition"
          offset-y
          min-width="auto"
        >
          <template v-slot:activator="{ on, attrs }">
            <v-combobox
              v-model="dates"
              multiple
              chips
              small-chips
              label="Select Date Range"
              prepend-icon="mdi-calendar"
              readonly
              v-bind="attrs"
              v-on="on"
            ></v-combobox>
          </template>
          <v-date-picker v-model="dates" range no-title scrollable>
            <v-spacer></v-spacer>
            <v-btn text color="primary" @click="menu = false"> Cancel </v-btn>
            <v-btn
              text
              color="primary"
              @click="changeDateRange(dates, 'by-type')"
            >
              OK
            </v-btn>
          </v-date-picker>
        </v-menu>
      </v-row>
      <v-data-table
        :headers="appointmentsByTypeHeaders"
        :items="appointmentsByTypeData"
        :items-per-page="10"
        class="elevation-1 mt-6"
        :loading="appointmentsByTypeLoading"
      ></v-data-table>

      <AppointmentTypeChart
        class="my-6"
        v-if="appointmentsByTypeData && appointmentsByTypeData.length > 0"
        :chartData="appointmentsByTypeData"
        label="Appointment Type Report"
      />
    </div>
    <div class="mx-4 mt-10 appointment-amounts">
      <h1 class="mb-10">Appointments Finanace Report</h1>
      <v-row class="ml-1">
        <v-select
          v-model="selectedYear"
          :items="years"
          label="Select a year"
          outlined
          @change="getAppointmentAmountsByYear"
        ></v-select>
      </v-row>
      <v-data-table
        :headers="appointmentAmountsHeaders"
        :items="appointmentAmountsData"
        :items-per-page="10"
        class="elevation-1 mt-6"
        :loading="appointmentAmountsLoading"
      ></v-data-table>
      <AppointmentFinanaceChart
        class="my-6"
        v-if="appointmentAmountsData && appointmentAmountsData.length > 0"
        :chartData="appointmentAmountsData"
        label="Appointments Finanace Report"
      />
    </div>
  </div>
</template>
<script>
import moment from 'moment'
import reports from '@/assets/js/api/reports'
import AppointmentTypeChart from './charts/AppointmentTypeChart.vue'
import AppointmentFinanaceChart from './charts/AppointmentFinanaceChart.vue'
export default {
  name: 'AppointmentReports',
  mixins: [reports],
  data: () => ({
    appointmentsByTypeLoading: false,
    appointmentAmountsLoading: false,
    dates: [],
    years: [],
    selectedYear: new Date().getFullYear(),
    menu: false,
    appointmentsByTypeHeaders: [
      {
        text: 'Appointment Type',
        align: 'start',
        sortable: true,
        value: 'typeText'
      },
      { text: 'Count', value: 'count', sortable: true }
    ],
    appointmentAmountsHeaders: [
      {
        text: 'Month',
        align: 'start',
        sortable: true,
        value: 'monthText'
      },
      { text: 'Count', value: 'count', sortable: true },
      { text: 'Amount', value: 'amount', sortable: true }
    ],
    appointmentsByTypeData: [],
    appointmentAmountsData: []
  }),
  methods: {
    changeDateRange(dates, type) {
      if (type === 'by-type') {
        this.$refs.menu.save(dates)
        this.getAppointsByType()
      }
    },
    async getAppointsByType() {
      this.appointmentsByTypeLoading = true
      this.appointsByType(
        {
          url: '/Report/appointsByType',
          method: 'POST',
          data: {
            StartDate: this.dates[0],
            EndDate: this.dates[1]
          }
        },
        response => {
          this.appointmentsByTypeData = response.data
          this.appointmentsByTypeLoading = false
        },
        error => {
          this.appointmentsByTypeLoading = false
          console.error(error)
        }
      )
    },
    getAppointmentAmountsByYear() {
      this.appointmentAmountsLoading = true
      this.appointmentAmountsByYear(
        {
          url: '/Report/appointmentAmountsByYear',
          method: 'POST',
          data: {
            Year: this.selectedYear
          }
        },
        response => {
          this.appointmentAmountsData = response.data
          this.appointmentAmountsLoading = false
        },
        error => {
          this.appointmentAmountsLoading = false
          console.error(error)
        }
      )
    },
    generateArrayOfYears(howMany) {
      var max = new Date().getFullYear()
      var min = max - howMany
      var years = []
      for (var i = max; i >= min; i--) {
        years.push(i)
      }
      return years
    }
  },
  created() {
    this.dates.push(
      moment().add(-1, 'M').format('YYYY-MM-DD'),
      moment().format('YYYY-MM-DD')
    )
    this.years = this.generateArrayOfYears(20)
    this.getAppointsByType()
    this.getAppointmentAmountsByYear()
  },
  components: { AppointmentTypeChart, AppointmentFinanaceChart }
}
</script>
<style lang="scss"></style>
