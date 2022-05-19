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
            <div class="sign-header">Sign up to account</div>
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
                :append-icon="show1 ? 'mdi-eye' : 'mdi-eye-off'"
                :type="show1 ? 'text' : 'password'"
                :error-messages="passwordErrors"
                label="password"
                required
                @input="$v.password.$touch()"
                @blur="$v.password.$touch()"
                @click:append="show1 = !show1"
                class="mb-2"
                counter
              ></v-text-field>
              <v-text-field
                v-model="cPassword"
                :append-icon="show2 ? 'mdi-eye' : 'mdi-eye-off'"
                :type="show2 ? 'text' : 'password'"
                :error-messages="cPasswordErrors"
                label="confirm password"
                required
                @input="$v.cPassword.$touch()"
                @blur="$v.cPassword.$touch()"
                @click:append="show2 = !show2"
                class="mb-2"
                counter
              ></v-text-field>
              <div class="pt-2">
                <router-link class="font-roboto redirect-link" to="/signIn"
                  >Already have an account? login one within
                  seconds</router-link
                >
              </div>
              <div class="mt-2">
                <v-btn class="mr-4 mt-5" @click="submit" color="green">
                  Sign up
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
import { required, minLength, email, sameAs } from 'vuelidate/lib/validators'
import user from '@/assets/js/api/user'
export default {
  mixins: [validationMixin, user],
  name: 'signIn',
  validations: {
    password: { required, minLength: minLength(8) },
    cPassword: { required, sameAsPassword: sameAs('password') },
    email: { required, email }
  },
  data: () => ({
    email: '',
    password: '',
    cPassword: '',
    show1: false,
    show2: false
  }),
  computed: {
    passwordErrors() {
      const errors = []
      if (!this.$v.password.$dirty) return errors
      !this.$v.password.minLength &&
        errors.push('Password must be at least 8 characters long')
      !this.$v.password.required && errors.push('Password is required.')
      return errors
    },
    cPasswordErrors() {
      const errors = []
      if (!this.$v.cPassword.$dirty) return errors
      !this.$v.cPassword.sameAsPassword &&
        errors.push('Confirm password must match with the password')
      !this.$v.cPassword.required &&
        errors.push('Confirm password is required.')
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
        this.signUp()
      }
    },
    clear() {
      this.$v.$reset()
      this.password = ''
      this.cPassword = ''
      this.email = ''
    },
    async signUp() {
      await this.emailRegister(
        {
          url: '/Authenticate/register',
          method: 'POST',
          data: {
            Email: this.email,
            Password: this.password
          }
        },
        response => {
          if (response.status === 200) {
            this.$router.push('/signIn')
          } else {
            console.error(response)
          }
        },
        error => {
          console.error(error.response.data.errors)
          error.response.data.errors.forEach(error => {
            this.$toast.error(error.description)
          })
        }
      )
    }
  }
}
</script>
<style scoped lang="scss">
@import '@/assets/styles/signUpAndSignIn.scss';
</style>
