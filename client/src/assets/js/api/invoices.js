import baseApiExecuter from '@/assets/js/api/baseExecuter'
export default {
  mixins: [baseApiExecuter],
  methods: {
    async getInvoices(data, successCallback, errorCallback) {
      await this.callSaloonApi(
        {
          url: data.url,
          data: data.data,
          method: data.method
        },
        response => successCallback(response),
        error => errorCallback(error)
      )
    },
    async getOrderInvoices(data, successCallback, errorCallback) {
      await this.callSaloonApi(
        {
          url: data.url,
          data: data.data,
          method: data.method
        },
        response => successCallback(response),
        error => errorCallback(error)
      )
    },
    async getAppointmentInvoices(data, successCallback, errorCallback) {
      await this.callSaloonApi(
        {
          url: data.url,
          data: data.data,
          method: data.method
        },
        response => successCallback(response),
        error => errorCallback(error)
      )
    }
  }
}
