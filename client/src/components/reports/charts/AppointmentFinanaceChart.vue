<script>
import { Bar, mixins } from 'vue-chartjs'
const { reactiveProp } = mixins
export default {
  extends: Bar,
  name: 'AppointmentFinanaceChart',
  mixins: [reactiveProp],
  props: {
    chartData: {
      type: Array,
      required: true,
      default: () => []
    },
    label: {
      type: String,
      required: true,
      default: undefined
    }
  },
  data: () => ({}),
  methods: {},
  mounted() {
    const chartdata2 = {
      labels: this.chartData.map(d => d.monthText),
      datasets: [
        {
          label: this.label,
          backgroundColor: [
            '#965b1e',
            '#9dba93',
            '#998c67',
            '#91a58a',
            '#c7d9c1'
          ],
          data: this.chartData.map(d => d.amount)
        }
      ]
    }
    const options = {
      responsive: true,
      maintainAspectRatio: false
    }
    this.renderChart(chartdata2, options)
  }
}
</script>

<style lang="scss" scoped></style>
