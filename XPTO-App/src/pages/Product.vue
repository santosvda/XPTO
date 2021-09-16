<template>
<div class="q-pa-md">
    <q-table
      :data="data"
      :columns="columns"
      row-key="title"
      :filter="filter"
      :separator="separator"
      no-data-label="I didn't find anything for you"
      no-results-label="The filter didn't uncover any results"
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
                <q-btn color="yellow" round dense icon="eva-edit-2-outline" />
              </q-item-section>

              <q-item-section>
                Editar
              </q-item-section>
            </q-item>
            <q-item clickable v-ripple @click="openModal(props.row)">
              <q-item-section avatar>
                <q-btn color="red" round dense icon="eva-trash-2-outline" />
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
        <q-input borderless dense debounce="300" v-model="filter" placeholder="Search">
          <template v-slot:append>
            <q-icon name="search" />
          </template>
        </q-input>
      </template>
    </q-table>
  </div>
</template>


<script>
export default {
  name: 'Product',
  data () {
    return {
        filter: '',
        separator: 'cell',
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
            {
            title: 'Notebook ASUS Aspire-5',
            price: 5000,
            barCode: 3123123123123
            },
            {
            title: 'Cadeira MX-5',
            price: 800,
            barCode: 654654654654654
            },
            {
            title: 'Memória RAM 8GB 3000mhz',
            price: 350.32,
            barCode: 654654654654654654654654654654
            }
        ]
    }
    },
    methods:{
        openModal(data){
            console.log(data)
        }
    }
}
</script>
