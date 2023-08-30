import { defineStore } from "pinia";
import { ref } from "vue";
import { fetchUserIp } from "../ip/api";

export const useTechMetaStore = defineStore("tech-meta", () => {
  const ip = ref("");

  async function fetchIp() {
    let userIp;
    try {
      userIp = await fetchUserIp();
    } catch (error) {
      return;
    }

    ip.value = userIp;
  }

  return {
    ip,
    fetchIp,
  };
});
