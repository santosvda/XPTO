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
          TODO - Form Editar
        </q-card-section>
        
        <q-card-actions align="right" class="text-grey-10">
          <q-btn class="bg-green" icon-right="eva-save" flat label="Salvar" v-close-popup />
        </q-card-actions>

        <q-card-actions align="right" class="text-black-10">
          <q-btn class="bg-red btn btn-lg" icon-right="eva-slash" flat label="Cancelar" v-close-popup />
        </q-card-actions>
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
        { name: 'price', align: 'center', label: 'Preço', field: 'price', sortable: true},
        { name: 'barcode', label: 'Código de Barras', field: 'barCode', sortable: true },
        ],
        data: [
        ]
    }
    },
    methods:{
        openModal(data){
            console.log(data)
            this.card = true
            this.modelo = data
        },
        getAllProducts(){
          this.$axios
            .get('http://localhost:5000/api/Product')
            .then(response => {
              console.log(response.data)
              this.data = response.data
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
