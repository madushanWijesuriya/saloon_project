import baseApiExecuter from '@/assets/js/api/baseExecuter'

export default {
  mixins: [baseApiExecuter],
  methods: {
    async placeOrder(data, successCallback, errorCallback) {
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
    async getOrders(data, successCallback, errorCallback) {
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
    async ChangeOrderStatus(data, successCallback, errorCallback) {
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
    async GenerateInvoiceForOrder(data, successCallback, errorCallback) {
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
    async GetInvoicesForOrder(data, successCallback, errorCallback) {
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
