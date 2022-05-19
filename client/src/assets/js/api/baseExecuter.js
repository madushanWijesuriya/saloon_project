import axios from 'axios'
export default {
  data() {
    return {
      saloonApiUrl: null
    }
  },
  computed: {
    user() {
      return this.$store.state.user.user
    }
  },
  methods: {
    async callSaloonApi(object, successCallback, errorCallback) {
      try {
        const apiObject = await {
          baseURL: this.saloonApiUrl,
          method: object.method || 'GET',
          url: object.url,
          headers: {
            Authorization: `Bearer ${
              this.user && this.user.token
                ? this.user.token
                : object.data && object.data.token
                ? object.data.token
                : ''
            }`,
            ...(object.headers || {})
          }
        }
        if (object.data) {
          apiObject.data = await object.data
        }
        const response = await axios(apiObject)
        successCallback(response)
      } catch (error) {
        if (
          error &&
          error.response &&
          error.response.status &&
          error.response.status === 401
        ) {
          this.$store.dispatch('logoutUser')
          this.$router.push('/signIn')
        }
        errorCallback(error)
      }
    }
  },
  created() {
    this.saloonApiUrl = 'https://localhost:44327/api'
  }
}
