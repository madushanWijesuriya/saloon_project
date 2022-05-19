<template>
  <div v-if="comment" class="comment">
    <div class="subtitle-2 pr-3 text-part">
      {{ comment.commentText }}
    </div>
    <v-btn
      v-if="isCurrentUserRelatedComment || isUserAdmin"
      class="delete-btn"
      text
      icon
      :color="'red'"
      @click="deleteComment"
    >
      <v-icon small>mdi-delete</v-icon>
    </v-btn>
  </div>
</template>
<script>
import comment from '@/assets/js/api/comment'
export default {
  name: 'comment',
  mixins: [comment],
  props: {
    comment: {
      type: Object,
      required: true,
      default: () => undefined
    },
    createdUserId: {
      type: String,
      required: true,
      default: ''
    }
  },
  data: () => ({}),
  methods: {
    deleteComment() {
      this.$emit('delete-comment', this.comment.id)
    }
  },
  computed: {
    user() {
      return this.$store.state.user.user
    },
    isUserAdmin() {
      return this.user && this.user.userRoles
        ? this.user.userRoles.some(role => role === 'Admin')
        : false
    },
    isCurrentUserRelatedComment() {
      return this.createdUserId === this.user.id
    }
  }
}
</script>
<style lang="scss">
.comment {
  display: flex;
  align-items: center;
  border: 1px solid gray;
  border-radius: 8px;
  margin-top: 14px;
  padding: 2px 10px;
  height: 45px;

  .text-part {
    max-width: 350px;
    overflow: hidden;
    white-space: nowrap;
    text-overflow: ellipsis;
  }
}
.delete-btn {
  margin-left: auto;
}
.comment:hover {
  background: rgba($color: #ffffff, $alpha: 0.3);
}
</style>
