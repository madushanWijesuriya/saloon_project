import baseApiExecuter from '@/assets/js/api/baseExecuter'

export default {
  mixins: [baseApiExecuter],
  methods: {
    async getProducts(data, successCallback, errorCallback) {
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
    async postProduct(data, successCallback, errorCallback) {
      await this.callSaloonApi(
        {
          url: data.url,
          data: data.data,
          method: data.method,
          headers: data.headers
        },
        response => successCallback(response),
        error => errorCallback(error)
      )
    },
    async deleteProductFromDB(data, successCallback, errorCallback) {
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
    async updateProductRequest(data, successCallback, errorCallback) {
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
