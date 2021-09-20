<template>
  <div class="fullscreen bg-blue text-white text-center q-pa-md flex flex-center">
    <div>
      <div style="font-size: 30vh" class="large-screen-only">
        404
      </div>
      <div style="font-size: 10vh" class="small-screen-only">
        404
      </div>

      <div class="text-h2" style="opacity:.4">
        Oops. Não tem nada aqui!!
      </div>

      <q-btn
        class="q-mt-xl"
        color="white"
        text-color="blue"
        unelevated
        to="/"
        label="Voltar"
        no-caps
      />
    </div>
  </div>
</template>

<script>
export default {
  name: 'Error404'
}
</script>
