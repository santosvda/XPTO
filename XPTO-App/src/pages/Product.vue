<template>
<div class="q-pa-md">
    <q-table
      :data="data"
      :columns="columns"
      row-key="title"
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
        <q-input borderless dense debounce="300" v-model="filter" placeholder="Search">
          <template v-slot:append>
            <q-icon name="search" />
          </template>
        </q-input>
      </template>
    </q-table>

    <q-dialog v-model="card">
      <q-card class="my-card">
        <q-img src="https://cdn.quasar.dev/img/chicken-salad.jpg" />

        <q-card-section>
          <q-btn
            fab
            color="primary"
            icon="place"
            class="absolute"
            style="top: 0; right: 12px; transform: translateY(-50%);"
          />

          <div class="row no-wrap items-center">
            <div class="col text-h6 ellipsis">
              Cafe Basilico
            </div>
            <div class="col-auto text-grey text-caption q-pt-md row no-wrap items-center">
              <q-icon name="place" />
              250 ft
            </div>
          </div>

        </q-card-section>

        <q-card-section class="q-pt-none">
          <div class="text-subtitle1">
            $・Italian, Cafe
          </div>
          <div class="text-caption text-grey">
            Small plates, salads & sandwiches in an intimate setting.
          </div>
        </q-card-section>

        <q-separator />

        <q-card-actions align="right">
          <q-btn v-close-popup flat color="primary" label="Reserve" />
          <q-btn v-close-popup flat color="primary" round icon="event" />
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
                id: 1,
                title: 'Notebook ASUS Aspire-5',
                price: 5000,
                barCode: 3123123123123
            },
            {
                id: 2, 
                title: 'Cadeira MX-5',
                price: 800,
                barCode: 654654654654654
            },
            {
                id: 3, 
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
            this.card = true
        }
    }
}
</script>
