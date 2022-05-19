<template>
  <div v-if="product" class="comments">
    <!-- <pre>{{ product }}</pre> -->
    <div v-for="comment in product.comments" :key="comment.id">
      <Comment
        v-if="comment"
        :comment="comment.comment"
        :createdUserId="comment.UserId"
        @delete-comment="deleteComment"
      />
    </div>
    <div class="mt-7 text-h6" v-if="product.comments.length === 0">
      No comments yet
    </div>
    <div class="mt-6">
      <CommentAdd
        v-if="product"
        :productId="product.id"
        @comment-added="commentAdded"
      />
    </div>
  </div>
</template>
<script>
import comment from '@/assets/js/api/comment'
import Comment from '../Comment.vue'
import CommentAdd from '../CommentAdd.vue'
export default {
  name: 'comments',
  mixins: [comment],
  props: {
    product: {
      type: Object,
      default: () => undefined,
      required: true
    }
  },
  data: () => ({}),
  methods: {
    commentAdded(comment) {
      this.product.comments.push(comment)
    },
    deleteComment(commentId) {
      this.commentRemove(
        {
          url: `/Comment/${commentId}`,
          method: 'DELETE'
        },
        () => {
          this.product.comments = this.product.comments.filter(
            c => c.commentId !== commentId
          )
        },
        error => console.error(error)
      )
    }
  },
  components: { Comment, CommentAdd }
}
</script>
<style lang=""></style>
