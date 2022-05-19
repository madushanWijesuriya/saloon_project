<script>
import { Bar, mixins } from 'vue-chartjs'
const { reactiveProp } = mixins
export default {
  extends: Bar,
  name: 'AppointmentTypeChart',
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
      labels: this.chartData.map(d => d.typeText),
      datasets: [
        {
          label: this.label,
          backgroundColor: [
            '#003f5c',
            '#58508d',
            '#bc5090',
            '#ff6361',
            '#ffa600'
          ],
          data: this.chartData.map(d => d.count)
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
