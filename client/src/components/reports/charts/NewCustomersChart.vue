<script>
import { Doughnut, mixins } from 'vue-chartjs'
const { reactiveProp } = mixins
export default {
  extends: Doughnut,
  name: 'NewCustomersChart',
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
            '#0095A8',
            '#112E51',
            '#FF7043',
            '#78909C',
            '#C1D7F2'
          ],
          data: this.chartData.map(d => d.usersCount)
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
