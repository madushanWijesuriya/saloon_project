<script>
import { Bar, mixins } from 'vue-chartjs'
const { reactiveProp } = mixins
export default {
  extends: Bar,
  name: 'ProductSoldReport',
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
            '#d9ed92',
            '#99d98c',
            '#76c893',
            '#34a0a4',
            '#1a759f',
            '#184e77'
          ],
          data: this.chartData.map(d => d.productsSoldAmount)
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
