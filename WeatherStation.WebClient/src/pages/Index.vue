<template>
  <q-page class="flex flex-center">
    <!-- <img alt="Quasar logo" src="~assets/quasar-logo-full.svg" /> -->
    <last-record/>
  </q-page>
</template>

<style></style>


<script>
import LastRecord from "../components/LastRecord";
export default {
  name: "PageIndex",
  components: {
    LastRecord
  }
};
</script>
