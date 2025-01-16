export const IsEmptyObj = (input: object): boolean => {
  if (typeof input !== 'object' || input === null) return true;
  if (Object.keys(input).length > 0) return false;
  return true;
}

export const ISODateTimeToReadable = (isoString: string): string => {
  const options: Intl.DateTimeFormatOptions = {
    year: 'numeric',
    month: 'short',
    day: '2-digit',
    hour: '2-digit',
    minute: '2-digit',
    hour12: true,
  };

  const date = new Date(isoString);
  const formattedDate = new Intl.DateTimeFormat('en-US', options).format(date);
  const timeIndex = formattedDate.indexOf(',') + 2;
  const finalFormattedDate =
    `${formattedDate.substring(0, timeIndex)} ${formattedDate.substring(timeIndex).toLowerCase()}`;

  return finalFormattedDate;
};

export const DateTimeReadable = (datetime: Date): string => {
  const options: Intl.DateTimeFormatOptions = {
    year: 'numeric',
    month: 'short',
    day: '2-digit',
    hour: '2-digit',
    minute: '2-digit',
    hour12: true,
  };

  const formattedDate = new Intl.DateTimeFormat('en-US', options).format(datetime);
  const timeIndex = formattedDate.indexOf(',') + 2;
  const finalFormattedDate =
    `${formattedDate.substring(0, timeIndex)} ${formattedDate.substring(timeIndex).toLowerCase()}`;

  return finalFormattedDate;
}

// Method to check if two json objects have same properties and values
export const AreObjectsEqual = (obj1: Record<string, unknown>, obj2: Record<string, unknown>): boolean => {
  if (typeof obj1 !== 'object' || typeof obj2 !== 'object' || obj1 === null || obj2 === null) return false;
  if (Object.keys(obj1).length !== Object.keys(obj2).length) return false;

  for (const key in obj1) {
    if (obj1.hasOwnProperty(key) && obj2.hasOwnProperty(key)) {
      if (obj1[key] !== obj2[key]) return false;
    }
  }

  return true;
}