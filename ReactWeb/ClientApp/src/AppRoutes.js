import { Counter } from "./components/Counter";
import { FetchData } from "./components/FetchData";
import { Home } from "./components/Home";
import { CoinTracker } from "./components/CoinTracker";

const AppRoutes = [
  {
    index: true,
    element: <Home />

  },
  {
    path: '/counter',
    element: <Counter />
  },
  {
    path: '/fetch-data',
    element: <FetchData />
    }
    , {
        path: '/cointracker',
        element: <CoinTracker />
    }
]; 

export default AppRoutes;
