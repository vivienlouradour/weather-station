<template>
  <q-card v-bind:dark="isDarkMode" bordered v-bind:class="[isDarkMode ? 'bg-grey-9' : '', my-card]">
    <q-card-section>
      <div class="text-h6">Last record</div>
      <div class="text-subtitle2">{{ date }}</div>
    </q-card-section>

    <q-separator v-bind:dark="isDarkMode" inset/>

    <q-card-section>Temperature: {{ temperature }} °C</q-card-section>
    <q-card-section>Humidity: {{ humidity }} %</q-card-section>
  </q-card>
</template>

<script>
export default {
  data() {
    return {
      isDarkMode: false,
      temperature: 32.5,
      humidity: 54,
      date: "22/06/2019 19:11"
    };
  }
};
</script>

<style lang="stylus" scoped>
.my-card {
  width: 100%;
  max-width: 200px;
}
</style>
