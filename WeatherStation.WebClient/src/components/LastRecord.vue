<template>
    <div>
    <h1>Last record</h1>
    <p> {{ message }} s</p>
    </div>
</template>

<script>
import axios from 'axios';

export default {
  name: 'LastRecord',
  data: function() {
    return {
      message: 'hello'
    }
  },
  mounted(){
      axios
      .get('http://localhost:5000/weatherstation/api/record/broadcasterTest')
      .then(response => (this.message = response))
      .catch(function (error){
          console.log('error axios : ' + error)
        })
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>