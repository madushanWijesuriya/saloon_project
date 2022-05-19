<template>
  <v-text-field
    ref="newCommentRef"
    v-model="comment"
    label="type new comment"
    autofocus
    v-on:keyup.enter="addComment"
  ></v-text-field>
</template>
<script>
import comment from '@/assets/js/api/comment'
export default {
  name: 'CommentAdd',
  mixins: [comment],
  props: {
    productId: {
      type: Number,
      default: undefined,
      required: true
    }
  },
  data: () => ({
    comment: null
  }),
  methods: {
    addComment() {
      if (!this.comment) return
      this.commentAdd(
        {
          url: `/Comment/${this.productId}`,
          method: 'POST',
          data: {
            CommentText: this.comment
          }
        },
        response => {
          this.comment = ''
          this.$emit('comment-added', response.data)
        },
        error => console.error(error)
      )
    }
  },
  mounted() {}
}
</script>
<style lang=""></style>
