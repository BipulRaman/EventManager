import { createContext, useContext, useCallback, useState } from "react";

const UpdateKeyContext = createContext(() => {
  console.warn("UpdateKeyContext.Provider was not found");
});

const useUpdateKeyContext = () => useContext(UpdateKeyContext);

const useKeyContext = () => {
  const [key, setKey] = useState<number>(Date.now());
  const updateKey = useCallback(() => {
    setKey(Date.now());
  }, []);

  return { key, updateKey };
};

export { UpdateKeyContext, useKeyContext, useUpdateKeyContext };