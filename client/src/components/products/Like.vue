<template>
  <v-row align="center" class="mx-0" v-if="product">
    <v-btn
      class=""
      text
      icon
      :color="isAlreadyLikedByCurrentUser ? '#224638' : 'Gray'"
      @click.stop="
        !isAlreadyLikedByCurrentUser ? incrementLike() : removeLike()
      "
    >
      <v-icon small>mdi-thumb-up</v-icon>
    </v-btn>
    <b class="mt-1"
      ><h4>{{ product.likes.length }}</h4></b
    >
  </v-row>
</template>
<script>
import like from '@/assets/js/api/like'
export default {
  name: 'Like',
  mixins: [like],
  props: {
    product: {
      type: Object,
      required: true,
      default: () => undefined
    }
  },
  data: () => ({
    //
  }),
  computed: {
    user() {
      return this.$store.state.user.user
    },
    isAlreadyLikedByCurrentUser() {
      if (!this.product) return false
      return this.product.likes.some(l => l.userId === this.user.id)
    }
  },
  methods: {
    async incrementLike() {
      if (this.isAlreadyLikedByCurrentUser) return
      await this.likeAdd(
        {
          url: `Like/${this.product.id}`,
          method: 'POST'
        },
        response => {
          this.product.likes.push(response.data)
        },
        error => console.error(error)
      )
    },
    async removeLike() {
      if (!this.isAlreadyLikedByCurrentUser) return
      const like = this.product.likes.find(l => l.userId === this.user.id)
      if (!like) {
        console.error('like not found')
        return
      }
      await this.likeRemove(
        {
          url: `Like/${like.id}`,
          method: 'DELETE'
        },
        () => {
          this.product.likes = this.product.likes.filter(
            l => l.userId !== this.user.id
          )
        },
        error => console.error(error)
      )
    }
  }
}
</script>
<style lang="scss"></style>
