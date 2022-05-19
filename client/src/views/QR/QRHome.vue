<template>
  <div class="QR">
    <v-row justify="center">
      <h2 class="my-8">
        You can Download | Print | Save your uniqe QR code here
      </h2>
    </v-row>

    <v-row justify="start">
      <v-card class="ml-8 mt-6">
        <img :src="qrImageUrl" width="350" alt="qr-code" id="printMe" />
      </v-card>
      <div class="ml-8 mt-6">
        <v-btn class="mt-5 white--text" color="green" @click="downloadQR"
          >Download</v-btn
        >
        <v-spacer></v-spacer>
        <!-- <v-btn class="mt-5 white--text" color="blue" @click="print"
          >Print &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</v-btn
        > -->
      </div>
    </v-row>
  </div>
</template>
<script>
import QR from '@/assets/js/api/QR'
import imageHelper from '@/assets/js/healpers/imageHelper'

export default {
  name: 'QRHome',
  mixins: [QR, imageHelper],
  data: () => ({
    qrImageUrl: '#'
  }),
  methods: {
    generateQrCode() {
      this.getQrCode(
        {
          url: '/QR',
          method: 'GET'
        },
        response => {
          this.qrImageUrl = `data:image/png;base64,${response.data}`
        },
        error => {
          console.error(error)
        }
      )
    },
    async print() {
      await this.$htmlToPaper('printMe')
    },
    downloadQR() {
      const file = this.dataURLtoFile(this.qrImageUrl, 'qr-code.png')
      this.saveFile(file, 'qr-code.png')
    },
    saveFile(blob, filename) {
      if (window.navigator.msSaveOrOpenBlob) {
        window.navigator.msSaveOrOpenBlob(blob, filename)
      } else {
        const a = document.createElement('a')
        document.body.appendChild(a)
        const url = window.URL.createObjectURL(blob)
        a.href = url
        a.download = filename
        a.click()
        setTimeout(() => {
          window.URL.revokeObjectURL(url)
          document.body.removeChild(a)
        }, 0)
      }
    }
  },
  created() {
    this.generateQrCode()
  }
}
</script>
<style lang="scss" scoped></style>
