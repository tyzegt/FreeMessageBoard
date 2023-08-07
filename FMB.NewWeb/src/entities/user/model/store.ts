import { defineStore } from "pinia";
import { ref } from "vue";


export const useUserStore = defineStore('user', () => {
    const name = ref('')

    return {
        name
    }
})