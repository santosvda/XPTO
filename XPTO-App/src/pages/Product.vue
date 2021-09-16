<template>
<div class="q-pa-md">
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
            <q-item clickable v-ripple @click="openModal(props.row)">
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

    <q-dialog
      v-model="card"
      full-height
      full-width
    >
      <q-card class="column full-height" style="width: 300px">
        <q-card-section>
          <div class="text-h6">Titulo</div>
        </q-card-section>

        <q-card-section class="col q-pt-none">
           <q-form
              @submit="onSubmit"
              @reset="onReset"
              class="q-gutter-md"
            >
              <q-input
                filled
                v-model="modelo.title"
                label="Produto *"
                hint="Nome do Produto"
                lazy-rules
                :rules="[ val => val && val.length > 0 || 'Informe o nome do produto']"
              />

              <q-input
                onkeypress='return event.charCode >= 48 && event.charCode <= 57'
                filled
                type="text"
                v-model="modelo.price"
                label="Preço *"
                mask="#.##"
                fill-mask="0"
                reverse-fill-mask
                lazy-rules
                :rules="[
                  val => val !== null && val !== '' || 'Informe um preço',
                  val => val > 0 && val < 99999999.99 || 'Preço máximo 99999999.99'
                ]"
              />

              <div class="text-grey-10">
                <q-btn label="Enviar" type="submit" color="green-6"/>
                <q-btn label="Apagar" type="reset" color="grey-7" class="q-ml-sm q-mr-sm" />
                <q-card-action class="text-grey-10">
                  <q-btn icon-right="eva-slash" label="Cancelar" color="red-6"  v-close-popup />
                </q-card-action>
              </div>
            </q-form>
        </q-card-section>
      </q-card>
    </q-dialog>
  </div>
</template>


<script>
export default {
  name: 'Product',
  data () {
    return {
        filter: '',
        separator: 'cell',
        card: false,
        modelo: {},
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
            console.log(data)
            this.card = true
            this.modelo = Object.assign({}, data)
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
        }
    },
    beforeMount() {
      this.getAllProducts()
    }
}
</script>
