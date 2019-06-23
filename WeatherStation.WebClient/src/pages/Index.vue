<template>
  <!-- <q-page class="flex flex-center"> -->
  <q-page>
    <div class="column">
      <div class="row justify-center items-center q-gutter-xl">
        <!-- <img alt="Quasar logo" src="~assets/quasar-logo-full.svg" /> -->
        <last-record/>
        <hottest-record/>
        <coldest-record/>
      </div>
    </div>
  </q-page>
</template>

<style></style>


<script>
import LastRecord from "../components/LastRecord";
import ColdestRecord from "../components/ColdestRecord";
import HottestRecord from "../components/HottestRecord";

export default {
  name: "PageIndex",
  components: {
    LastRecord,
    ColdestRecord,
    HottestRecord
  }
};
</script>
