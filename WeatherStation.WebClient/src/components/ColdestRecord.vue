<template>
  <q-card
    v-bind:dark="isDarkMode"
    bordered
    v-bind:class="[isDarkMode ? 'bg-grey-9' : '', 'my-card']"
  >
    <q-card-section>
      <div class="text-h6">Coldest Record</div>
      <div class="text-subtitle2">{{ record && record.dateTime }}</div>
    </q-card-section>

    <q-separator v-bind:dark="isDarkMode" inset/>

    <q-card-section>Temperature: {{ record && record.temperature }} °C</q-card-section>
    <q-card-section>Humidity: {{ record && record.humidity }} %</q-card-section>
  </q-card>
</template>

<script>
import apiService from "../services/weatherStationApi";
export default {
  data() {
    return {
      isDarkMode: false,
      record: null
    };
  },

  created() {
    this.fetchLastRecord();
  },

  methods: {
    fetchLastRecord() {
      return apiService
        .getColdest(process.env.BROADCASTER_NAME)
        .then(result => {
          this.record = result;
        });
    }
  }
};
</script>

<style lang="stylus" scoped>
.my-card {
  width: 100%;
  max-width: 200px;
}
</style>
