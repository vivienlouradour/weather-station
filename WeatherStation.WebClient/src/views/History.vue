<template>
  <div class="history">
    <h1>History page</h1>
    {{ message }}s
    {{ test}}
  </div>
</template>

<script>
import config from '@/config.js'

export default {
  name: 'history',
  data: function() {
    return {
      message: 'hello',
      test: config.API_URL + '/record/broadcasterTest',
    }
  },

}
</script>
