<template>
  <div class="home">
    <img alt="Vue logo" src="../assets/meteo.png">
    <LastRecord />
    <!-- <HelloWorld msg="Last record"/> -->
  </div>
</template>

<script>
// @ is an alias to /src
import HelloWorld from '@/components/HelloWorld.vue'
import LastRecord from '@/components/LastRecord.vue'

export default {
  name: 'home',
  components: { 
    HelloWorld,
    LastRecord
  }
}
</script>
