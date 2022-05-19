<template>
  <div>
    <div class="mx-4 mt-10 appointment-amounts">
      <h1 class="mb-10">Product Sold Report</h1>
      <v-row class="ml-1">
        <v-select
          v-model="selectedYear"
          :items="years"
          label="Select a year"
          outlined
          @change="getHowManyProductSoldByMonth"
        ></v-select>
      </v-row>
      <v-data-table
        :headers="howManyProductSoldByMonthHeaders"
        :items="howManyProductSoldByMonthData"
        :items-per-page="10"
        class="elevation-1 mt-6"
        :loading="howManyProductSoldByMonthLoading"
      ></v-data-table>
      <ProductSoldReport
        class="my-6"
        v-if="
          howManyProductSoldByMonthData &&
          howManyProductSoldByMonthData.length > 0
        "
        :chartData="howManyProductSoldByMonthData"
        label="Product Sold Report"
      />
    </div>
  </div>
</template>
<script>
import reports from '@/assets/js/api/reports'
import ProductSoldReport from './charts/ProductSoldReport.vue'
export default {
  name: 'ProductReports',
  mixins: [reports],
  data: () => ({
    selectedYear: new Date().getFullYear(),
    years: [],
    howManyProductSoldByMonthData: [],
    howManyProductSoldByMonthLoading: false,
    howManyProductSoldByMonthHeaders: [
      {
        text: 'Month',
        align: 'start',
        sortable: true,
        value: 'monthText'
      },
      {
        text: 'Products Sold Count',
        value: 'productsSoldCount',
        sortable: true
      },
      {
        text: 'Products Sold Amount (LKR)',
        value: 'productsSoldAmount',
        sortable: true
      }
    ]
  }),
  methods: {
    generateArrayOfYears(howMany) {
      var max = new Date().getFullYear()
      var min = max - howMany
      var years = []
      for (var i = max; i >= min; i--) {
        years.push(i)
      }
      return years
    },
    getHowManyProductSoldByMonth() {
      this.howManyProductSoldByMonthLoading = true
      this.howManyProductSoldByMonth(
        {
          url: '/Report/howManyProductSoldByMonth',
          method: 'POST',
          data: {
            Year: this.selectedYear
          }
        },
        response => {
          this.howManyProductSoldByMonthData = response.data
          this.howManyProductSoldByMonthLoading = false
        },
        error => {
          this.howManyProductSoldByMonthLoading = false
          console.error(error)
        }
      )
    }
  },
  created() {
    this.years = this.generateArrayOfYears(20)
    this.getHowManyProductSoldByMonth()
  },
  components: { ProductSoldReport }
}
</script>
<style lang="scss"></style>
