<template>
  <v-dialog
    transition="dialog-top-transition"
    max-width="1000"
    v-model="dialog"
  >
    <template v-slot:default="dialog">
      <v-card>
        <v-toolbar color="primary" dark>Product Details</v-toolbar>
        <v-card-text>
          <v-row>
            <v-col md="6" lg="6" class="mt-5">
              <v-carousel :show-arrows="false">
                <v-carousel-item
                  v-for="(item, i) in items"
                  :key="i"
                  :src="item.src"
                ></v-carousel-item>
              </v-carousel>
            </v-col>
            <v-col md="6" lg="6" class="mt-5">
              <div class="details-wrapper ml-5">
                <div class="text-h4">{{ product.name }}</div>
                <v-row>
                  <v-col cols="3" class="my-3">
                    <Like :product="product" />
                  </v-col>
                  <v-col cols="6">
                    <div class="subtitle-1 mt-2">
                      <b>Rs.{{ product.price }}</b>
                    </div>
                  </v-col>
                </v-row>
                <v-row>
                  <div class="ml-4">{{ product.description }}</div>
                </v-row>
                <v-row class="comment-outer-wrapper">
                  <Comments class="ml-4" v-if="product" :product="product" />
                </v-row>
              </div>
            </v-col>
          </v-row>
        </v-card-text>
        <v-card-actions class="justify-end">
          <v-btn text @click="dialog.value = false">Close</v-btn>
        </v-card-actions>
      </v-card>
    </template>
  </v-dialog>
</template>
<script>
import Like from '../Like.vue'
import Comments from './Comments.vue'
export default {
  props: {
    product: {
      type: Object,
      required: true,
      default: () => undefined
    }
  },
  data: () => ({
    dialog: false,
    items: [
      {
        src: 'https://images.pexels.com/photos/234220/pexels-photo-234220.jpeg?auto=compress&cs=tinysrgb&dpr=2&h=750&w=1260'
      }
    ]
  }),
  methods: {
    openModal() {
      this.dialog = true
      this.$nextTick(() => {
        if (this.product && this.product.productImages.length > 0) {
          this.items = this.product.productImages.map(i => {
            return {
              src: require(`../../../../../saloonAPI/saloonAPI/Images/${i.uniqueName}`)
            }
          })
        }
      })
    },
    closeModal() {
      this.dialog = false
    }
  },
  components: { Like, Comments }
}
</script>
<style lang="scss">
.comment-outer-wrapper {
  margin-top: 35px !important;
  max-height: 370px;
  overflow-y: scroll;
}
</style>
