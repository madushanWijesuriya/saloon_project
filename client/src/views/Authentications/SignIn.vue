<template>
  <div>
    <v-container :fluid="true" class="sign-in-wrapper">
      <v-row>
        <v-col cols="12" lg="3" md="3" class="left-side">
          <div class="welcome-text">Hello, Welcome to Nayanathara Salons</div>
          <div class="welcome-text-2">
            <i
              >Join with us by filling details and feel the hight quality
              service</i
            >
          </div>
        </v-col>
        <v-col class="right-side">
          <div class="sign-box-wrapper">
            <div class="sign-header">Sign in to account</div>
            <form>
              <v-text-field
                v-model="email"
                :error-messages="emailErrors"
                label="email"
                required
                @input="$v.email.$touch()"
                @blur="$v.email.$touch()"
                class="mb-2"
              ></v-text-field>
              <v-text-field
                v-model="password"
                :error-messages="passwordErrors"
                :append-icon="show1 ? 'mdi-eye' : 'mdi-eye-off'"
                :type="show1 ? 'text' : 'password'"
                label="password"
                required
                @input="$v.password.$touch()"
                @blur="$v.password.$touch()"
                class="mb-2"
                @click:append="show1 = !show1"
              ></v-text-field>
              <div class="pt-2">
                <router-link class="font-roboto redirect-link" to="/signUp"
                  >Don't have an account? Create one within seconds</router-link
                >
              </div>
              <div class="mt-2">
                <v-btn
                  class="mr-4 mt-5"
                  @click="submit"
                  color="green"
                  :loading="signInLoading"
                >
                  Sign in
                </v-btn>
                <v-btn class="mt-5" @click="clear"> clear </v-btn>
              </div>
            </form>
          </div>
        </v-col>
      </v-row>
    </v-container>
  </div>
</template>
<script>
import { validationMixin } from 'vuelidate'
import { required, email } from 'vuelidate/lib/validators'
import user from '@/assets/js/api/user'
export default {
  mixins: [validationMixin, user],
  name: 'signIn',
  validations: {
    password: { required },
    email: { required, email }
  },
  data: () => ({
    password: '',
    email: '',
    show1: false,
    signInLoading: false
  }),
  computed: {
    passwordErrors() {
      const errors = []
      if (!this.$v.password.$dirty) return errors
      !this.$v.password.required && errors.push('Password is required.')
      return errors
    },
    emailErrors() {
      const errors = []
      if (!this.$v.email.$dirty) return errors
      !this.$v.email.email && errors.push('Must be valid e-mail')
      !this.$v.email.required && errors.push('E-mail is required')
      return errors
    }
  },
  methods: {
    submit() {
      this.$v.$touch()
      if (!this.$v.$anyError) {
        this.signIn()
      }
    },
    clear() {
      this.$v.$reset()
      this.password = ''
      this.email = ''
    },
    async signIn() {
      this.signInLoading = true
      await this.emailLogin(
        {
          url: '/Authenticate/login',
          method: 'POST',
          data: {
            Email: this.email,
            Password: this.password
          }
        },
        response => {
          this.signInLoading = false
          this.$store.dispatch('loginUser', response.data)
          this.$router.push('/')
        },
        error => {
          this.signInLoading = false
          console.error(error.response)
          if (error.response.status)
            this.$toast.error('Please enter valid email and password')
        }
      )
    }
  }
}
</script>
<style scoped lang="scss">
@import '@/assets/styles/signUpAndSignIn.scss';
</style>
