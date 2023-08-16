import { http } from "~/shared/api";

type Ip = {
  ip: string;
};

export async function fetchUserIp(): Promise<string> {
  const { data } = await http("https://api.ipify.org/?format=json")
    .get()
    .json<Ip>();
  return data.value?.ip || "";
}
