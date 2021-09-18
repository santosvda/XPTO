<template>
<div class="q-pa-md">
  <div class="q-mb-sm">
    <q-btn label="Novo produto" @click="openModal(null)" color="green-6"/>
  </div>

    <q-table
      :data="data"
      :columns="columns"
      row-key="id"
      :filter="filter"
      :separator="separator"
      no-data-label="Desculpe, não encontramos nenhum produto!"
      no-results-label="O filtro preenchido não retornou nenhum produto!"
    >
    <template v-slot:header="props">
        <q-tr :props="props">
          <q-th auto-width />
          <q-th
            v-for="col in props.cols"
            :key="col.name"
            :props="props"
          >
            {{ col.label }}
          </q-th>
        </q-tr>
      </template>

      <template v-slot:body="props">
        <q-tr :props="props">
          <q-td auto-width>
            <q-btn size="sm" color="accent" round dense @click="props.expand = !props.expand" :icon="props.expand ? 'remove' : 'add'" />
          </q-td>
          <q-td
            v-for="col in props.cols"
            :key="col.name"
            :props="props"
          >
            {{ col.value }}
          </q-td>
        </q-tr>
        <q-tr v-show="props.expand" :props="props">
          <q-td colspan="100%" class="justify-evenly">
            <q-list padding class="menu-list">
            <q-item clickable v-ripple  @click="openModal(props.row)" >
              <q-item-section avatar>
                <q-btn color="orange-7" round dense icon="eva-edit-2-outline" />
              </q-item-section>

              <q-item-section>
                Editar
              </q-item-section>
            </q-item>
            <q-item clickable v-ripple @click="removeModal(props.row.id)">
              <q-item-section avatar>
                <q-btn color="red-10" round dense icon="eva-trash-2-outline" />
              </q-item-section>

              <q-item-section>
                Remover
              </q-item-section>
            </q-item>
            </q-list>
          </q-td>
        </q-tr>
      </template>
    <template v-slot:top-right>
        <q-input borderless dense debounce="300" v-model="filter" placeholder="Procurar">
          <template v-slot:append>
            <q-icon name="search" />
          </template>
        </q-input>
      </template>
    </q-table>

    <div>
       <q-dialog
      v-model="card"
      full-height
      full-width
    >
      <q-card class="  column full-height" style="width: 300px">
        <q-card-section>
          <div class="text-h6">{{modalTitle}}</div>
        </q-card-section>

        <q-card-section class="col q-mb-sm q-pt-none">
           <q-form
              @submit="onSubmit"
              @reset="onReset"
              class="q-gutter-md"
            >
              <q-input
                filled
                v-model="product.title"
                label="Produto *"
                hint="Nome do Produto"
                lazy-rules
                :rules="[val => val.length >= 5 && val.length <= 200 || 'Quantidade de carateres do produto precisa estar entre 5 e 200'
                ]"
              />

              <q-input
                onkeypress='return event.charCode >= 48 && event.charCode <= 57'
                filled
                type="text"
                v-model="product.price"
                label="Preço(R$) *"
                hint="Preço unitário do produto"
                mask="#.##"
                fill-mask="0"
                reverse-fill-mask
                lazy-rules
                :rules="[
                  val => val !== null && val !== '' || 'Informe um preço',
                  val => val < 99999999.99 || 'Preço máximo R$ 99999999.99'
                ]"
              />
              <q-input
                filled
                type="text"
                v-model="product.barCode"
                label="Código de Barras"
                hint="Código de barras do produto"
                lazy-rules
                :rules="[
                  val => val.length <= 200 || 'Código de barras excedeu o limite de 200 caracteres'
                ]"
              />
              <div class="">
                <q-file
                  class="q-mt-lg"
                  v-model="imageUpload"
                  @input="captureImage"
                  label="Escolha uma imagem"
                  accept="image/*"
                  outlined
                >
                  <template v-slot:prepend>
                    <q-icon name="eva-attach-outline" />
                  </template>
                </q-file>
              </div>
            <div class="box row justify-center q-pa-md">
              <canvas
                v-show="imageCaptured"
                ref="canvas"
                class="q-canvas"
                height="240"
              />
            </div>
              <div class="row justify-center q-pd-sm">
                <q-btn icon-right="eva-cloud-upload-outline" class="col-3" label="Enviar" type="submit" color="green-6"/>
                <q-btn icon-right="eva-backspace-outline" label="Apagar" type="reset" color="grey-7" class="col-3 q-ml-sm q-mr-sm" />
                <q-btn icon-right="eva-slash" label="Cancelar" color="red-6" class="col-3"  v-close-popup />
              </div>
            </q-form>
        </q-card-section>
      </q-card>
    </q-dialog>
    </div>
  </div>
</template>


<script>
export default {
  name: 'Product',
  data () {
    return {
        filter: '',
        separator: 'cell',
        modalTitle: '',
        card: false,
        create: false,
        product: {},
        imageUpload: [],
        imageCaptured: false,
        columns: [
        {
        name: 'title',
        required: true,
        label: 'Produto',
        align: 'left',
        field: row => row.title,
        format: val => `${val}`,
        sortable: true,
        classes: 'bg-grey-2 ellipsis',
        style: 'max-width: 100px',
        headerClasses: 'bg-primary text-white'
        },
        { name: 'price', align: 'left', label: 'Preço (R$)', field: 'price', sortable: true},
        { name: 'barcode', align: 'left', label: 'Código de Barras', field: 'barCode', sortable: true },
        ],
        data: [
        ]
    }
    },
    methods:{
        openModal(data){
            if(data == null){
              this.card = true
              this.product = {title: '', price: 0.00, barCode: '', image64: ''}
              this.modalTitle = 'Cadastrar um novo produto'
              this.create = true;
            }else{
              this.card = true
              this.product = Object.assign({}, data)
              this.modalTitle = `Editando: ${data.title}`
              this.create = false
              this.$nextTick(function () {this.showImage()})
            }
        },
        getAllProducts(){
          this.$axios
            .get('http://localhost:5000/api/Product')
            .then(response => {
              console.log(response.data)
              this.data = response.data
              //formata o preço com as casas decimais
              this.data.forEach(element => {
                element.price = element.price.toFixed(2)
              });
            }).catch(err => {
              console.log('err: ', err)
            })
        },
        createProduct() {
           this.$q.loading.show()

            this.$axios.post(`${ process.env.API }/product`, this.product).then(response => {
              console.log('response: ', response)
              this.$q.notify({
                color: 'green-4',
                textColor: 'white',
                icon: 'eva-cloud-upload',
                message: 'Produto Salvo!',
                actions: [
                  { label: 'Fechar', color: 'white' }
                ]
              })
              this.$q.loading.hide()
              if (this.$q.platform.is.safari) {
                setTimeout(() => {
                  window.location.href = '/'
                }, 1000)
              }
              this.getAllProducts()
            }).catch(err => {
              console.log('err: ', err)
              this.$q.notify({
                message: 'Erro!',
                actions: [
                  { label: 'Dismiss', color: 'white' }
                ]
              })
              this.$q.loading.hide()
              if (this.$q.platform.is.safari) {
                setTimeout(() => {
                  window.location.href = '/'
                }, 1000)
              }
            })

            this.card = false;
        },
        updateProduct(){          
            this.$q.loading.show()

            this.$axios.put(`${ process.env.API }/product/${this.product.id}`, this.product).then(response => {
              console.log('response: ', response)
              this.$q.notify({
                color: 'green-4',
                textColor: 'white',
                icon: 'eva-cloud-upload',
                message: 'Produto Salvo!',
                actions: [
                  { label: 'Fechar', color: 'white' }
                ]
              })
              this.$q.loading.hide()
              this.getAllProducts()
            }).catch(err => {
              console.log('err: ', err)
              this.$q.notify({
                color: 'red-7',
                textColor: 'white',
                icon: 'eva-cloud-upload',
                message: 'Ops! Algo deu errado!',
                message: 'Erro!',
                actions: [
                  { label: 'Fechar', color: 'white' }
                ]
              })
              this.$q.loading.hide()
            })

            this.card = false;
        },
        removeModal(id){
          console.log(id)
          this.$q.dialog({
          title: 'Confirmar',
          message: `Tem certeza que deseja remover esse produto?`,
          color: 'negative',
          ok: `Sim, tenho certeza`,
          cancel: true,
          default: 'Cancelar'   // <<<<
        }).onOk(() => this.removeObject(id) )
           
        },
        removeObject(id){
            this.$q.loading.show()

            this.$axios.delete(`${ process.env.API }/product/${id}`).then(response => {
              console.log('response: ', response)
              this.$q.notify({
                color: 'red',
                textColor: 'white',
                icon: 'eva-cloud-upload',
                message: 'Produto Removido!',
                actions: [
                  { label: 'Fechar', color: 'white' }
                ]
              })
              this.$q.loading.hide()
              this.getAllProducts()
            }).catch(err => {
              console.log('err: ', err)
              this.$q.notify({
                color: 'red-7',
                textColor: 'white',
                icon: 'eva-cloud-upload',
                message: 'Ops! Algo deu errado!',
                message: 'Erro!',
                actions: [
                  { label: 'Fechar', color: 'white' }
                ]
              })
              this.$q.loading.hide()
            })

            this.card = false;
        },
        onSubmit () {
          console.log(this.product)
          if(this.create){
            this.createProduct()
          }else{
            this.updateProduct()
          }
          this.clean()
        },
        onReset () {
          this.product = {title: '', price: 0.00, barCode: '', image64: ''}
          this.clean()
        },
        captureImage(file) {
          this.clean()
          let canvas = this.$refs.canvas
          let context = canvas.getContext('2d')

          var reader = new FileReader()
          reader.onload = event => {
            var img = new Image()
            img.onload = () => {
              canvas.height = img.height
              canvas.width = img.width
              context.drawImage(img,0,0,canvas.width,canvas.height)
              this.imageCaptured = true
            }
            img.src = event.target.result
            this.product.image64 = event.target.result
          }
          reader.readAsDataURL(file)
        },
        dataURLtoFile(dataurl, filename) {
 
          var arr = dataurl.split(','),
              mime = arr[0].match(/:(.*?);/)[1],
              bstr = atob(arr[1]), 
              n = bstr.length, 
              u8arr = new Uint8Array(n);
              
          while(n--){
              u8arr[n] = bstr.charCodeAt(n);
          }
          
          return new File([u8arr], filename, {type:mime});
      },
      showImage(){
          let canvas = this.$refs.canvas
          let context = canvas.getContext('2d')
          var reader = new FileReader()
          reader.onload = event => {
            var img = new Image()
            img.onload = () => {
              canvas.width = img.width
              canvas.height = img.height
              context.drawImage(img,0,0)
              this.imageCaptured = true
            }
            img.src = event.target.result
            this.product.image64 = event.target.result
          }
          reader.readAsDataURL(this.dataURLtoFile('data:image/png;base64,'+this.product.image64,'img.png'))
      },
      clean(){
          this.imageUpload = []
          let canvas = this.$refs.canvas
          let context = canvas.getContext('2d')
          context.clearRect(0, 0, canvas.width, canvas.height);
          this.imageCaptured = false
      }
    },
    beforeMount() {
      this.getAllProducts()
    }
}
</script>

<style lang="sass">
.camera-frame
  margim-top: -5px
  border: 2px solid $grey-10
  border-radius: 5px
.q-canvas
  max-width: 40%
  max-height: 300px
#box
    overflow: auto
</style>
