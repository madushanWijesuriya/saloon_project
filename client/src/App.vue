<template>
  <v-app id="app">
    <NavBar v-if="!isMenuHide" />
    <router-view />
  </v-app>
</template>

<script>
import NavBar from '@/components/NavBar.vue'
export default {
  components: {
    NavBar
  },
  data() {
    return {
      //
    }
  },
  computed: {
    isMenuHide() {
      return this.$store.state.isMenuHide
    }
  }
}
</script>

<style lang="scss">
@import './assets/styles/custome.scss';
</style>
